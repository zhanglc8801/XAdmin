using XAdmin.SqlSugar;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.IRepository;

namespace XAdmin.Repository
{
    /// <summary>
    /// 网站信息管理数据操作类
    /// </summary>
    public class SiteInfoManagementRepository: ISiteInfoManagementRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        #region 获取站点栏目列表
        /// <summary>
        /// 获取站点栏目列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Site_Channel>> GetSiteChannelList(Site_ChannelQuery query)
        {
            var channelList = db.Queryable<Site_Channel>().Where(x => x.SiteID == query.SiteID);
            if (!string.IsNullOrWhiteSpace(query.Name))
                channelList = channelList.Where(x => x.Name.Contains(query.Name));
            if (!string.IsNullOrWhiteSpace(query.Keywords))
                channelList = channelList.Where(x => x.Name.Contains(query.Keywords));
            if (query.ContentType > 0)
                channelList = channelList.Where(x => x.ContentType == query.ContentType);
            if (query.Status != null)
                channelList = channelList.Where(x => x.Status == query.Status);
            return await channelList.ToListAsync();
        }
        #endregion

        #region 递归获取当前栏目及其上级栏目
        /// <summary>
        /// 递归获取当前栏目及其上级栏目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ChannelModel GetChannelList(Site_ChannelQuery query)
        {
            ChannelModel model = new ChannelModel();
            Site_Channel channel = db.Queryable<Site_Channel>().Where(x => x.SiteID == query.SiteID && x.ID == query.ID).First();
            model.ID = channel.ID;
            model.Name = channel.Name;
            model.ParentID = channel.ParentID;
            model.SiteID = channel.SiteID;
            model.ContentType = channel.ContentType;
            model.ParentList = GetParentList(model);
            return model;
        }

        private List<ChannelModel> GetParentList(ChannelModel model)
        {
            List<ChannelModel> rootNodeList = new List<ChannelModel>();
            var childList = db.Queryable<Site_Channel>().Where(x =>x.SiteID==model.SiteID && x.ID== model.ParentID).ToList();
            foreach (Site_Channel item in childList)
            {
                ChannelModel treeItem = new ChannelModel();
                treeItem.ID = item.ID;
                treeItem.Name = item.Name;
                treeItem.ParentID = item.ParentID;
                treeItem.SiteID = item.SiteID;
                treeItem.ContentType = item.ContentType;
                treeItem.ParentList = GetParentList(treeItem);
                rootNodeList.Add(treeItem);
            }
            return rootNodeList;
        }

        #endregion

        #region 获取栏目实体
        /// <summary>
        /// 获取栏目实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<Site_Channel> GetSiteChannelModel(Site_ChannelQuery query)
        {
            return await db.Queryable<Site_Channel>().Where(x => x.SiteID == query.SiteID && x.ID == query.ID).FirstAsync();
        } 
        #endregion

        #region 组装上级栏目下拉框
        /// <summary>
        /// 组装上级栏目下拉框
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeSelectModel> MakeChannelSelect(QueryBase query)
        {
            List<Site_Channel> menuList = await db.Queryable<Site_Channel>().Where(x => x.SiteID == query.SiteID && x.Status == true).OrderBy("SortID ASC").ToListAsync();
            TreeSelectModel model = new TreeSelectModel();

            Site_Channel channle = new Site_Channel();
            channle.ID = 0;
            model.TreeSelectList = InitTreeSelect(menuList, channle);
            return model;
        }
        #endregion

        #region 生成树形菜单下拉框

        private List<TreeSelectItem> InitTreeSelect(List<Site_Channel> channelList, Site_Channel channel)
        {
            List<TreeSelectItem> rootNodeList = new List<TreeSelectItem>();

            var list = channelList.Where(x => x.ParentID == channel.ID).ToList();
            foreach (Site_Channel item in list)
            {
                TreeSelectItem treeItem = new TreeSelectItem();
                treeItem.id = item.ID;
                treeItem.name = item.Name;
                treeItem.open = false;
                treeItem.@checked = false;
                treeItem.children = InitTreeSelect(channelList, item);
                rootNodeList.Add(treeItem);
            }
            return rootNodeList;
        }
        #endregion

        #region 更改栏目状态
        /// <summary>
        /// 更改栏目状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeChannelStatus(Site_Channel model)
        {
            ExecResult result = new ExecResult();
            int i = 0;
            if (model.Name == "Status")
                i = await db.Updateable<Site_Channel>().SetColumns(x => new Site_Channel { Status = model.Status }).Where(x => x.ID == model.ID).ExecuteCommandAsync();
            if (model.Name == "IsNav")
                i = await db.Updateable<Site_Channel>().SetColumns(x => new Site_Channel { IsNav = model.IsNav }).Where(x => x.ID == model.ID).ExecuteCommandAsync();

            if (i > 0)
                result.code = 1;
            else
                result.code = 0;
            return result;
        } 
        #endregion

        #region 保存网站栏目信息
        /// <summary>
        /// 保存网站栏目信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveChannel(Site_Channel model)
        {
            ExecResult result = new ExecResult();
            int count = 0;
            if (model.ID == 0)//添加栏目
                count = db.Insertable<Site_Channel>(model).IgnoreColumns(new string[] { "ID" }).ExecuteCommand();
            else
                count = db.Updateable<Site_Channel>(model).IgnoreColumns(new string[] { "AddDate" }).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "修改网站栏目成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "修改网站栏目失败！";
            }
            return result;
        }
        #endregion

        #region 获取栏目内容管理 左侧栏目菜单
        /// <summary>
        /// 获取栏目内容管理 左侧栏目菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<ChannelMenuModel> GetChannelMenuList(QueryBase query)
        {
            ChannelMenuEx list = new ChannelMenuEx();
            string sql = "select * from Site_Channel where SiteID='" + query.SiteID + "' and Status=1";
            if (query.IntValue != 1) //不是管理员组
            {
                sql = "select * from Site_Channel where SiteID='" + query.SiteID + "' and Status=1 and RoleIDs LIKE '%" + query.IntValue + ",%'";
            }
            list.menuList = db.Ado.SqlQuery<ChannelMenuModel>(sql).ToList();
            return list.menuList;
        }
        #endregion

        #region 更改各栏目内容状态
        /// <summary>
        /// 更改各栏目内容状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeChannelContentStatus(SiteChannelQuery query)
        {
            int i = 0;
            switch (query.ContentType)
            {
                case 1:
                    i = await db.Updateable<SiteChannel_DescriptionContent>().SetColumns(x => new SiteChannel_DescriptionContent { Status = query.Status }).Where(x => x.ID == query.ID).ExecuteCommandAsync();
                    break;
                case 2:
                    i = await db.Updateable<SiteChannel_NewsContent>().SetColumns(x => new SiteChannel_NewsContent { Status = query.Status }).Where(x => x.ID == query.ID).ExecuteCommandAsync();
                    break;
                case 3:
                    i = await db.Updateable<SiteChannel_ImageContent>().SetColumns(x => new SiteChannel_ImageContent { Status = query.Status }).Where(x => x.ID == query.ID).ExecuteCommandAsync();
                    break;
                case 4:
                    i = await db.Updateable<SiteChannel_AudioContent>().SetColumns(x => new SiteChannel_AudioContent { Status = query.Status }).Where(x => x.ID == query.ID).ExecuteCommandAsync();
                    break;
                case 5:
                    i = await db.Updateable<SiteChannel_VideoContent>().SetColumns(x => new SiteChannel_VideoContent { Status = query.Status }).Where(x => x.ID == query.ID).ExecuteCommandAsync();
                    break;
                case 6:
                    i = await db.Updateable<SiteChannel_FileContent>().SetColumns(x => new SiteChannel_FileContent { Status = query.Status }).Where(x => x.ID == query.ID).ExecuteCommandAsync();
                    break;
            }
            ExecResult result = new ExecResult();
            if (i > 0)
                result.code = 1;
            else
                result.code = 0;
            return result;
        }
        #endregion

        #region 描述类
        /// <summary>
        /// 获取描述类内容实体
        /// </summary>
        /// <returns></returns>
        public SiteChannel_DescriptionContent GetDescriptionModel(SiteChannelQuery query)
        {
            return db.Queryable<SiteChannel_DescriptionContent>().Where(x => x.SiteID == query.SiteID && x.ChannelID == query.ID).First();
        }

        /// <summary>
        /// 保存描述类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveDescription(SiteChannel_DescriptionContent model)
        {
            ExecResult result = new ExecResult();
            int i = 0;
            SiteChannel_DescriptionContent desc = db.Queryable<SiteChannel_DescriptionContent>().Where(x => x.SiteID == model.SiteID && x.ChannelID == model.ChannelID).First();
            if (desc == null)
                i = db.Insertable<SiteChannel_DescriptionContent>(model).ExecuteCommand();
            else
                i = db.Updateable<SiteChannel_DescriptionContent>(model).IgnoreColumns(new string[] { "ID", "AddTime", "Clicks" }).Where(x => x.ID == model.ID).ExecuteCommand();
            if (i > 0)
            {
                result.code = 1;
                result.msg = "保存成功";
            }
            else
            {
                result.code = 0;
                result.msg = "保存失败";
            }
            return result;
        }
        #endregion

        #region 新闻资讯类
        #region 获取新闻资讯分页列表
        /// <summary>
        /// 获取新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<NewsContentModel> GetNewsPageList(SiteChannelQuery query)
        {
            NewsContentModel model = new NewsContentModel();
            RefAsync<int> TotalNumber = 0;
            var where = db.Queryable<SiteChannel_NewsContent, Site_Channel, UserInfo>((nc, sc, ui) => new object[] { JoinType.Left, nc.ChannelID == sc.ID, JoinType.Left, nc.UserId == ui.ID }).Where((nc, sc, ui) => nc.SiteID == query.SiteID && nc.ChannelID == query.ID);

            if (!string.IsNullOrWhiteSpace(query.Title))
                where = where.Where((nc, sc, ui) => nc.Title.Contains(query.Title));
            if (!string.IsNullOrWhiteSpace(query.Abstruct))
                where = where.Where((nc, sc, ui) => nc.Abstruct.Contains(query.Abstruct));
            if (!string.IsNullOrWhiteSpace(query.Author))
                where = where.Where((nc, sc, ui) => nc.Author.Contains(query.Author));
            if (!string.IsNullOrWhiteSpace(query.Tags))
                where = where.Where((nc, sc, ui) => nc.Tags.Contains(query.Tags));
            if (!string.IsNullOrWhiteSpace(query.Content))
                where = where.Where((nc, sc, ui) => nc.Content.Contains(query.Content));
            var getAll = where.OrderBy((nc, sc, ui) => nc.ID).Select((nc, sc, ui) => new SiteChannel_NewsContentEx
            {
                ID = nc.ID,
                SiteID = nc.SiteID,
                ChannelID = nc.ChannelID,
                Title = nc.Title,
                TitleColor = nc.TitleColor,
                IsBold = nc.IsBold,
                IsItalic = nc.IsItalic,
                LinkUrl = nc.LinkUrl,
                Target = nc.Target,
                Source = nc.Source,
                Author = nc.Author,
                Tags = nc.Tags,
                Abstruct = nc.Abstruct,
                TitlePhoto = nc.TitlePhoto,
                //Content = nc.Content,
                UserId = nc.UserId,
                Status = nc.Status,
                AddTime = nc.AddTime,
                LastTime = nc.LastTime,
                Clicks = nc.Clicks,
                UserName = ui.UserName,
                RealName = ui.RealName,
                ChannelName = sc.Name
            });
            model.NewsContentList = await getAll.ToPageListAsync(query.CurrentPage, query.PageSize, TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        }
        #endregion

        #region 获取站点新闻资讯分页列表
        /// <summary>
        /// 获取站点新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<SiteChannel_NewsContent> GetSiteNewsPageList(SiteChannelQuery query)
        {
            var list = db.Queryable<SiteChannel_NewsContent>().IgnoreColumns("Content").Where(x => x.SiteID == query.SiteID && x.ChannelID == query.ID).ToPageList(query.CurrentPage, query.PageSize);
            return list;
        }
        #endregion

        #region 获取新闻资讯内容实体
        /// <summary>
        /// 获取新闻资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteChannel_NewsContent GetNewsModel(SiteChannelQuery query)
        {
            return db.Queryable<SiteChannel_NewsContent>().Where(x => x.SiteID == query.SiteID && x.ID == query.ID).First();
        }
        #endregion

        #region 保存新闻资讯信息
        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveNewsInfo(SiteChannel_NewsContent model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<SiteChannel_NewsContent>(model).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增新闻资讯成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增新闻资讯失败";
                }
            }
            else
            {
                int i = db.Updateable<SiteChannel_NewsContent>(model).IgnoreColumns(new string[] { "AddTime", "Clicks" }).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "修改新闻资讯成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "修改新闻资讯失败";
                }
            }
            return result;
        }
        #endregion

        #region 删除新闻资讯
        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteNews(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<long> list = new List<long>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(long.Parse(Id));
            }
            int count = db.Deleteable<SiteChannel_NewsContent>().Where(x => list.Contains(x.ID) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功";
            }
            else
            {
                result.code = 0;
                result.msg = "删除失败";
            }
            return result;
        }
        #endregion
        #endregion

        #region 图片类
        #region 获取图片资讯分页列表
        /// <summary>
        /// 获取图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ImageContentModel> GetImagePageList(SiteChannelQuery query)
        {
            ImageContentModel model = new ImageContentModel();
            RefAsync<int> TotalNumber = 0;
            var where = db.Queryable<SiteChannel_ImageContent, Site_Channel, UserInfo>((nc, sc, ui) => new object[] { JoinType.Left, nc.ChannelID == sc.ID, JoinType.Left, nc.UserID == ui.ID }).Where((nc, sc, ui) => nc.SiteID == query.SiteID && nc.ChannelID == query.ID);

            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                where = where.Where((nc, sc, ui) => nc.Title.Contains(query.KeyStr) || nc.Abstruct.Contains(query.KeyStr));

            var getAll = where.OrderBy((nc, sc, ui) => nc.ID).Select((nc, sc, ui) => new SiteChannel_ImageContentEx
            {
                ID = nc.ID,
                SiteID = nc.SiteID,
                ChannelID = nc.ChannelID,
                Title = nc.Title,
                LinkUrl = nc.LinkUrl,
                Target = nc.Target,
                Source = nc.Source,
                Author = nc.Author,
                Tags = nc.Tags,
                Abstruct = nc.Abstruct,
                ImagePath = nc.ImagePath,
                UserID = nc.UserID,
                Status = nc.Status,
                AddTime = nc.AddTime,
                LastTime = nc.LastTime,
                Clicks = nc.Clicks,
                UserName = ui.UserName,
                RealName = ui.RealName,
                ChannelName = sc.Name
            });
            model.ImageContentList = await getAll.ToPageListAsync(query.CurrentPage, query.PageSize, TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        }
        #endregion

        #region 获取站点图片资讯分页列表
        /// <summary>
        /// 获取站点图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ImageContentModel GetSiteImagePageList(SiteChannelQuery query)
        {
            ImageContentModel model = new ImageContentModel();
            int TotalNumber = 0;
            model.SiteImageContentList = db.Queryable<SiteChannel_ImageContent>().Where(x => x.SiteID == query.SiteID && x.ChannelID == query.ID).ToPageList(query.CurrentPage, query.PageSize,ref TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        }
        #endregion

        #region 获取图片资讯内容实体
        /// <summary>
        /// 获取图片资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteChannel_ImageContent GetImageModel(SiteChannelQuery query)
        {
            return db.Queryable<SiteChannel_ImageContent>().Where(x => x.SiteID == query.SiteID && x.ID == query.ID).First();
        }
        #endregion

        #region 保存图片资讯信息
        /// <summary>
        /// 保存图片资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveImageInfo(SiteChannel_ImageContent model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<SiteChannel_ImageContent>(model).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增图片资讯成功";
                    result.ReturnInt = i;
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增图片资讯失败";
                }
            }
            else
            {
                int i = db.Updateable<SiteChannel_ImageContent>(model).IgnoreColumns(new string[] { "AddTime", "Clicks" }).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "修改图片资讯成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "修改图片资讯失败";
                }
            }
            return result;
        }
        #endregion

        #region 删除图片资讯
        /// <summary>
        /// 删除图片资讯
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteImage(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<long> list = new List<long>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(long.Parse(Id));
            }
            int count = db.Deleteable<SiteChannel_ImageContent>().Where(x => list.Contains(x.ID) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功";
            }
            else
            {
                result.code = 0;
                result.msg = "删除失败";
            }
            return result;
        }
        #endregion 
        #endregion

        #region 视频类
        /// <summary>
        /// 获取视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<VideoContentModel> GetVideoPageList(SiteChannelQuery query)
        {
            VideoContentModel model = new VideoContentModel();
            RefAsync<int> TotalNumber = 0;
            var where = db.Queryable<SiteChannel_VideoContent, Site_Channel, UserInfo>((nc, sc, ui) => new object[] { JoinType.Left, nc.ChannelID == sc.ID, JoinType.Left, nc.UserID == ui.ID }).Where((nc, sc, ui) => nc.SiteID == query.SiteID && nc.ChannelID == query.ID);

            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                where = where.Where((nc, sc, ui) => nc.Title.Contains(query.KeyStr) || nc.Abstruct.Contains(query.KeyStr));

            var getAll = where.OrderBy((nc, sc, ui) => nc.ID).Select((nc, sc, ui) => new SiteChannel_VideoContentEx
            {
                ID = nc.ID,
                SiteID = nc.SiteID,
                ChannelID = nc.ChannelID,
                Title = nc.Title,
                VideoImage = nc.VideoImage,
                VideoPath = nc.VideoPath,
                Abstruct=nc.Abstruct,
                FileID = nc.FileID,
                Size = nc.Size,
                Length=nc.Length,
                Source = nc.Source,
                Author = nc.Author,
                Tags = nc.Tags,
                Status = nc.Status,
                UserID = nc.UserID,
                AddTime = nc.AddTime,
                LastTime = nc.LastTime,
                Clicks = nc.Clicks,
                UserName = ui.UserName,
                RealName = ui.RealName,
                ChannelName = sc.Name
            });
            model.VideoContentList = await getAll.ToPageListAsync(query.CurrentPage, query.PageSize, TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        }

        /// <summary>
        /// 获取站点视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<SiteChannel_VideoContent> GetSiteVideoPageList(SiteChannelQuery query)
        {
            var list = db.Queryable<SiteChannel_VideoContent>().Where(x => x.SiteID == query.SiteID && x.ChannelID == query.ID).ToPageList(query.CurrentPage, query.PageSize);
            return list;
        }

        /// <summary>
        /// 获取视频内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteChannel_VideoContent GetVideoModel(SiteChannelQuery query)
        {
            return db.Queryable<SiteChannel_VideoContent>().Where(x => x.SiteID == query.SiteID && x.ID == query.ID).First();
        }

        /// <summary>
        /// 保存视频
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveVideoInfo(SiteChannel_VideoContent model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<SiteChannel_VideoContent>(model).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增视频成功";
                    result.ReturnInt = i;
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增视频失败";
                }
            }
            else
            {
                int i = db.Updateable<SiteChannel_VideoContent>(model).IgnoreColumns(new string[] { "AddTime", "Clicks" }).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "修改视频成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "修改视频失败";
                }
            }
            return result;
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteVideo(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<long> list = new List<long>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(long.Parse(Id));
            }
            int count = db.Deleteable<SiteChannel_VideoContent>().Where(x => list.Contains(x.ID) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功";
            }
            else
            {
                result.code = 0;
                result.msg = "删除失败";
            }
            return result;
        }

        #endregion

        #region 网站内容预定
        #region 获取预定分页列表
        /// <summary>
        /// 获取预定分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public YuDingModel GetYuDingPageList(YuDingQuery query)
        {
            YuDingModel model = new YuDingModel();
            int TotalNumber = 0;
            var where = db.Queryable<YuDing>();

            if (!string.IsNullOrWhiteSpace(query.Name))
                where = where.Where(x => x.Name.Contains(query.Name));
            if (!string.IsNullOrWhiteSpace(query.Phone))
                where = where.Where(x => x.Phone.Contains(query.Phone));
            if (!string.IsNullOrWhiteSpace(query.ChannelName))
                where = where.Where(x => x.ChannelName.Contains(query.ChannelName));
            if (!string.IsNullOrWhiteSpace(query.Content))
                where = where.Where(x => x.Content.Contains(query.Content));

            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                where = where.Where(x => x.ChannelName.Contains(query.KeyStr) || x.Content.Contains(query.KeyStr));


            if (query.Status > -1)
                where = where.Where(x => x.Status == query.Status);
            if (query.ChannelID > 0)
                where = where.Where(x => x.ChannelID == query.ChannelID);
            if (query.ContentID > 0)
                where = where.Where(x => x.ContentID == query.ContentID);

            model.YuDingList = where.OrderBy("Status DESC").ToPageList(query.CurrentPage, query.PageSize, ref TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        }
        #endregion

        #region 获取预定内容实体
        /// <summary>
        /// 获取预定内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public YuDing GetYuDingModel(YuDingQuery query)
        {
            return db.Queryable<YuDing>().Where(x => x.ID == query.ID).First();
        }
        #endregion

        #region 保存预定信息
        /// <summary>
        /// 保存预定信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveYuDing(YuDing model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<YuDing>(model).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增预定成功";
                    result.ReturnInt = i;
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增预定失败";
                }
            }
            return result;
        }
        #endregion

        #region 设置预定信息状态
        /// <summary>
        /// 设置预定信息状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SetYuDingStatus(YuDing model)
        {
            ExecResult result = new ExecResult();
            if (model.ID > 0)
            {
                int i = db.Updateable<YuDing>(model).UpdateColumns(x => new { x.Status, x.ActivityText }).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "设置预定状态成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "设置预定状态失败";
                }
            }
            return result;
        }
        #endregion

        #region 删除预定信息
        /// <summary>
        /// 删除预定信息
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteYuDing(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<long> list = new List<long>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(long.Parse(Id));
            }
            int count = db.Deleteable<YuDing>().Where(x => list.Contains(x.ID)).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功";
            }
            else
            {
                result.code = 0;
                result.msg = "删除失败";
            }
            return result;
        }
        #endregion  
        #endregion

    }
}
