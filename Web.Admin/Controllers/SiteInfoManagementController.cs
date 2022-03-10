using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using XAdmin.Common.Utils;

namespace Web.Admin.Controllers
{
    public class SiteInfoManagementController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ISiteInfoManagementService _siteInfoService;
        private readonly IResourcesService _resService;
        public SiteInfoManagementController(IWebHostEnvironment webHostEnvironment, ISiteInfoManagementService siteInfoService, IResourcesService resService)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._siteInfoService = siteInfoService;
            this._resService = resService;
        }

        /// <summary>
        /// 栏目列表 视图页
        /// </summary>
        /// <returns></returns>
        public IActionResult Channel()
        {
            return View();
        }

        /// <summary>
        /// 获取所有栏目列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetChannelList()
        {
            Site_ChannelQuery query = new Site_ChannelQuery();
            query.SiteID = CurUser.UserModel.SiteID;
            List<Site_Channel> list = await _siteInfoService.GetSiteChannelList(query);
            if (list.Count > 0)
                return Json(new { code = 0, data = list, msg = "", count = list.Count });
            else
                return Json(new { code = 1, msg = "没有获取到数据!" });
        }

        /// <summary>
        /// 修改栏目信息 视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChannelEdit(int ID = 0, int PID = 0)
        {
            Site_Channel model = new Site_Channel();
            if (ID != 0)
            {
                Site_ChannelQuery query = new Site_ChannelQuery();
                query.SiteID = CurUser.UserModel.SiteID;
                query.ID = ID;
                model = await _siteInfoService.GetSiteChannelModel(query);
                return View(model);
            }
            else
            {
                model.ID = 0;
                model.Name = "";
                model.Keywords = "";
                model.ParentID = PID;
                model.Description = "";
                model.IsNav = false;
                model.Status = true;
                return View(model);
            }
        }

        /// <summary>
        /// 组装上级栏目下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MakeChannelSelect()
        {
            QueryBase query = new QueryBase();
            query.SiteID = CurUser.UserModel.SiteID;
            TreeSelectModel model = await _siteInfoService.MakeChannelSelect(query);
            if (model != null && model.TreeSelectList.Count > 0)
                return Json(model.TreeSelectList);
            else
                return Json("");
        }

        /// <summary>
        /// 保存网站栏目信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isroot"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveChannelAjax(Site_Channel model, string isroot)
        {
            model.SiteID = CurUser.UserModel.SiteID;
            if (isroot == "on")
                model.ParentID = 0;
            ExecResult result = _siteInfoService.SaveChannel(model);
            return Json(new { code = result.code, msg = result.msg });
        }

        /// <summary>
        /// 更改栏目状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ChangeChannelStatus(Site_Channel model)
        {
            model.SiteID = CurUser.UserModel.SiteID;
            var data = await _siteInfoService.ChangeChannelStatus(model);
            return Json(new { code = 0, msg = "Success！" });
        }

        /// <summary>
        /// 栏目内容管理
        /// </summary>
        /// <returns></returns>
        public IActionResult ChannelContent()
        {
            //获取菜单
            StringBuilder _StringBuilder = new StringBuilder();
            CreateChannelMenus(0, _StringBuilder);
            string str = _StringBuilder.ToString();
            ViewData["MenuHtml"] = str;
            return View();
        }

        #region 创建栏目菜单
        private void CreateChannelMenus(int ParentID, StringBuilder _StringBuilder)
        {
            QueryBase query = new QueryBase();
            query.SiteID = CurUser.UserModel.SiteID;
            query.IntValue = CurUser.CurRole.RoleID;
            IList<ChannelMenuModel> list = _siteInfoService.GetChannelMenuList(query);
            var ChannelMenuList = new List<ChannelMenuModel>();
            if (ParentID == 0)
                ChannelMenuList = list.Where(x => x.ParentID == 0).ToList();
            else
                ChannelMenuList = list.Where(x => x.ParentID == ParentID).ToList();

            if (ChannelMenuList.Count > 0)
            {
                foreach (ChannelMenuModel item in ChannelMenuList)
                {
                    _StringBuilder.Append(string.Format("<li data-name=\"{0}\" class=\"layui-nav-item\">", item.id));
                    var _Child_List = list.Where(w => w.ParentID != 0 && w.ParentID == item.id).ToList();
                    if (_Child_List.Count > 0)
                    {
                        _StringBuilder.Append(string.Format("<a href=\"javascript:;\" lay-tips=\"{0}\" lay-direction=\"2\" title='{1}'>", item.Name, item.ContentTypeName.Replace("栏目", "") + "-" + item.Name));
                        _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe714;") + "</i>");
                        _StringBuilder.Append(string.Format("<cite>{0}</cite>", item.Name));
                        _StringBuilder.Append("</a>");
                        _StringBuilder.Append("<dl class=\"layui-nav-child\">");
                        foreach (ChannelMenuModel item2 in _Child_List)
                        {
                            _StringBuilder.Append(string.Format("<dd data-name=\"{0}\" >", item2.id));
                            var _ThirdList = list.Where(w => w.ParentID != 0 && w.ParentID == item2.id).ToList();
                            if (_ThirdList.Count > 0)
                            {
                                _StringBuilder.Append(string.Format("<a href=\"javascript:;\">{0}</a>", item2.Name));
                                _StringBuilder.Append("<dl class=\"layui-nav-child\">");
                                foreach (ChannelMenuModel item3 in _ThirdList)
                                {
                                    _StringBuilder.Append(string.Format("<dd data-name=\"{3}\"><a href=\"javascript:GoToUrl('{0}','{1}')\" title='{4}'>{2}</a></dd>", item3.id, item3.ContentType, item3.Name, item3.id, item3.ContentTypeName.Replace("栏目", "") + "-" + item3.Name));
                                }
                                _StringBuilder.Append("</dl>");
                            }
                            else
                            {
                                _StringBuilder.Append(string.Format("<a href=\"javascript:GoToUrl('{0}','{1}')\" title='{3}'>{2}</a>", item2.id, item2.ContentType, item2.Name, item2.ContentTypeName.Replace("栏目", "") + "-" + item2.Name));
                                #region 设置图标
                                if (item2.ContentTypeName.Contains("栏目目录"))
                                    _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe714;") + "</i>");
                                else if (item2.ContentTypeName.Contains("描述"))
                                    _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe62a;") + "</i>");
                                else if (item2.ContentTypeName.Contains("列表"))
                                    _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe649;") + "</i>");
                                else if (item2.ContentTypeName.Contains("图片"))
                                    _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe60d;") + "</i>");
                                else if (item2.ContentTypeName.Contains("文件"))
                                    _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe655;") + "</i>");
                                else if (item2.ContentTypeName.Contains("音频"))
                                    _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe645;") + "</i>");
                                else if (item2.ContentTypeName.Contains("视频"))
                                    _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe6ed;") + "</i>");
                                #endregion
                            }
                            _StringBuilder.Append("</dd>");
                        }
                        _StringBuilder.Append("</dl>");
                    }
                    else
                    {
                        _StringBuilder.Append(string.Format("<a href=\"javascript:GoToUrl('{0}','{1}')\" lay-tips=\"{2}\" lay-direction=\"2\" title='{3}'>", item.id, item.ContentType, item.Name, item.ContentTypeName.Replace("栏目", "") + "-" + item.Name));
                        #region 设置图标
                        if (item.ContentTypeName.Contains("栏目目录"))
                            _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe714;") + "</i>");
                        else if (item.ContentTypeName.Contains("描述"))
                            _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe62a;") + "</i>");
                        else if (item.ContentTypeName.Contains("列表"))
                            _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe649;") + "</i>");
                        else if (item.ContentTypeName.Contains("图片"))
                            _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe60d;") + "</i>");
                        else if (item.ContentTypeName.Contains("文件"))
                            _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe655;") + "</i>");
                        else if (item.ContentTypeName.Contains("音频"))
                            _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe645;") + "</i>");
                        else if (item.ContentTypeName.Contains("视频"))
                            _StringBuilder.Append("<i class=\"layui-icon\">" + WebUtility.UrlDecode("&#xe6ed;") + "</i>");
                        #endregion
                        _StringBuilder.Append(string.Format("<cite>{0}</cite>", item.Name));
                        _StringBuilder.Append("</a>");
                    }
                    _StringBuilder.Append("</li>");
                }
            }
        }
        #endregion

        #region 描述类管理
        /// <summary>
        /// 描述类列表页
        /// </summary>
        /// <param name="id">ChannelID</param>
        /// <returns></returns>
        public IActionResult DescriptionList(int id)
        {
            SiteChannelQuery query = new SiteChannelQuery();
            query.ID = id;
            query.SiteID = CurUser.UserModel.SiteID;
            SiteChannel_DescriptionContent model = _siteInfoService.GetDescriptionModel(query);
            if (model == null)
                model = new SiteChannel_DescriptionContent();
            return View(model);
        }

        /// <summary>
        /// 保存描述类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveDescriptionAjax(SiteChannel_DescriptionContent model, int ChannelID)
        {
            ExecResult result = new ExecResult();
            model.SiteID = CurUser.UserModel.SiteID;
            model.UserId = CurUser.UserModel.ID;
            model.AddTime = DateTime.Now;
            model.LastTime = DateTime.Now;
            result = _siteInfoService.SaveDescription(model);
            if (result.code == 1)
            {
                return Json(new { code = 1, msg = "保存成功。" });
            }
            else
                return Json(new { code = 0, msg = result.msg });
        } 
        #endregion

        #region 资讯类管理
        /// <summary>
        /// 资讯类列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult NewsList(int id)
        {
            ViewBag.id = id;
            return View();
        }

        #region 获取新闻资讯分页列表
        /// <summary>
        /// 获取新闻资讯分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetNewsPageList(int page, int limit, SiteChannelQuery query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            NewsContentModel model = await _siteInfoService.GetNewsPageList(query);
            if (model != null && model.NewsContentList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.NewsContentList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "没有获取到符合条件的数据！" });

        } 
        #endregion

        #region 新闻资讯新增/修改 视图页
        /// <summary>
        /// 新闻资讯新增/修改 视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<IActionResult> NewsEdit(long ID,int ChannelID)
        {
            SiteChannel_NewsContent model = new SiteChannel_NewsContent();
            model.ChannelID = ChannelID;
            if (ID != 0)
            {
                SiteChannelQuery query = new SiteChannelQuery();
                query.SiteID = CurUser.UserModel.SiteID;
                query.ID = ID;
                model = _siteInfoService.GetNewsModel(query);
                if (model != null)
                {
                    ResourcesQuery ResQuery = new ResourcesQuery();
                    ResQuery.SiteID = CurUser.UserModel.SiteID;
                    ResQuery.IDs = model.FileIDStr;
                    ViewBag.FileList = await _resService.GetResourcesList(ResQuery);
                }
                return View(model);
            }
            else
            {
                return View(model);
            }
        } 
        #endregion

        #region 保存新闻资讯信息
        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveNewsInfoAjax(SiteChannel_NewsContent model)
        {
            ExecResult result = new ExecResult();
            model.SiteID = CurUser.UserModel.SiteID;
            model.UserId = CurUser.UserModel.ID;
            model.AddTime = DateTime.Now;
            model.LastTime = DateTime.Now;
            result = _siteInfoService.SaveNewsInfo(model);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        #region 删除新闻资讯
        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteNewsAjax(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _siteInfoService.DeleteNews(query);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        #endregion

        /// <summary>
        /// 图片类列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult ImageList(int id)
        {
            ViewBag.id = id;
            return View();
        }

        #region 获取图片资讯分页列表
        /// <summary>
        /// 获取图片资讯分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetImagePageList(int page, int limit, SiteChannelQuery query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            ImageContentModel model = await _siteInfoService.GetImagePageList(query);
            if (model != null && model.ImageContentList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.ImageContentList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "没有获取到符合条件的数据！" });

        }
        #endregion

        #region 图片资讯新增/修改 视图页
        /// <summary>
        /// 图片资讯新增/修改 视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult ImageEdit(long ID, int ChannelID)
        {
            SiteChannel_ImageContent model = new SiteChannel_ImageContent();
            model.ChannelID = ChannelID;
            if (ID != 0)
            {
                SiteChannelQuery query = new SiteChannelQuery();
                query.SiteID = CurUser.UserModel.SiteID;
                query.ID = ID;
                model = _siteInfoService.GetImageModel(query);
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region 保存图片资讯
        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveImageInfoAjax(SiteChannel_ImageContent model)
        {
            ExecResult result = new ExecResult();
            model.SiteID = CurUser.UserModel.SiteID;
            model.UserID = CurUser.UserModel.ID;
            model.AddTime = DateTime.Now;
            model.LastTime = DateTime.Now;
            result = _siteInfoService.SaveImageInfo(model);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        #region 删除图片资讯
        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteImageAjax(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _siteInfoService.DeleteImage(query);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        /// <summary>
        /// 音频类列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult AudioList(int id)
        {
            return View();
        }

        /// <summary>
        /// 视频类列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult VideoList(int id)
        {
            ViewBag.id = id;
            return View();
        }

        #region 获取视频分页列表
        /// <summary>
        /// 获取图片资讯分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetVideoPageList(int page, int limit, SiteChannelQuery query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            VideoContentModel model = await _siteInfoService.GetVideoPageList(query);
            if (model != null && model.VideoContentList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.VideoContentList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "没有获取到符合条件的数据！" });

        }
        #endregion

        #region 视频新增/修改 视图页
        /// <summary>
        /// 视频新增/修改 视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult VideoEdit(long ID, int ChannelID)
        {
            SiteChannel_VideoContent model = new SiteChannel_VideoContent();
            model.ChannelID = ChannelID;
            if (ID != 0)
            {
                SiteChannelQuery query = new SiteChannelQuery();
                query.SiteID = CurUser.UserModel.SiteID;
                query.ID = ID;
                model = _siteInfoService.GetVideoModel(query);
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region 保存视频
        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SaveVideoInfoAjax(SiteChannel_VideoContent model)
        {
            ExecResult result = new ExecResult();
            model.SiteID = CurUser.UserModel.SiteID;
            model.UserID = CurUser.UserModel.ID;
            model.AddTime = DateTime.Now;
            model.LastTime = DateTime.Now;
            result = _siteInfoService.SaveVideoInfo(model);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        #region 删除视频
        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteVideoAjax(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _siteInfoService.DeleteVideo(query);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        /// <summary>
        /// 文件类列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult FileList()
        {
            return View();
        }

        /// <summary>
        /// 预定信息 视图页
        /// </summary>
        /// <returns></returns>
        public IActionResult YuDingList()
        {
            return View();
        }

        #region 获取预定信息分页列表
        /// <summary>
        /// 获取预定信息分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetYuDingPageList(int page, int limit, YuDingQuery query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            YuDingModel model = _siteInfoService.GetYuDingPageList(query);
            if (model != null && model.YuDingList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.YuDingList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "没有获取到符合条件的数据！" });

        }
        #endregion

        public IActionResult YuDingEdit(int ID)
        {
            YuDing model = new YuDing();
            if (ID != 0)
            {
                YuDingQuery query = new YuDingQuery();
                query.ID = ID;
                model = _siteInfoService.GetYuDingModel(query);
            }
            return View(model);
        }

        /// <summary>
        /// 设置预定信息状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetYuDingStatus(YuDing model)
        {
            ExecResult result = _siteInfoService.SetYuDingStatus(model);
            return Json(new { code = result.code, msg = result.msg });
        }

        #region 更改各栏目内容状态
        /// <summary>
        /// 更改各栏目内容状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ChangeChannelContentStatus(SiteChannelQuery query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            var data = await _siteInfoService.ChangeChannelContentStatus(query);
            return Json(new { code = 1, msg = "Success！" });
        } 
        #endregion

        #region 公共方法 获取视频文件信息
        /// <summary>
        /// 获取视频文件信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetVideoInfo(string videoPath)
        {
            string ffmpegPath = _webHostEnvironment.WebRootPath + "\\ffmpeg";
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            string thumbnail = videoPath.Substring(0, videoPath.LastIndexOf('/')) + "/VideoImage/" + Path.GetFileName(videoPath) + ".png";
            string ThumbnailPath = Path.GetFullPath(contentRootPath + thumbnail);
            videoPath = Path.GetFullPath(contentRootPath + videoPath);

            try
            {
                VideoInfo vi = await FFmpegUtil.GetVideoInfo(ffmpegPath, videoPath);
                FFmpegUtil.GetVideoThumbnail(ffmpegPath, videoPath, ThumbnailPath, 50);
                return Json(new { code = 1, length = vi.Length, height = vi.Height, width = vi.Width, Thumbnail = thumbnail });
            }
            catch (Exception ex)
            {
                return Json(new { code = 1, msg = ex.Message });
            }
        } 
        #endregion


    }
}
