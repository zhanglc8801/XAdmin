using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.Common;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;

namespace Web.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISiteInfoManagementService _siteInfoService;
        public HomeController(ISiteInfoManagementService siteInfoService, IResourcesService resService)
        {
            this._siteInfoService = siteInfoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ImageList(long ChannelID, int page = 1)
        {
            SiteModel siteModel = new SiteModel();
            siteModel.ConentEntity = new ContentModel();
            string SiteID = SiteConfigManager.SiteID;

            Site_ChannelQuery channelQuery = new Site_ChannelQuery();
            channelQuery.SiteID = SiteID;
            channelQuery.ID = ChannelID;
            Site_Channel channelEntity = await _siteInfoService.GetSiteChannelModel(channelQuery);
            if (channelEntity != null)
            {
                ViewBag.ChannelName = channelEntity.Name;
                ViewBag.ChannelID = channelEntity.ID;
                SiteChannelQuery query = new SiteChannelQuery();
                query.SiteID = SiteConfigManager.SiteID;
                query.ID = ChannelID;
                query.CurrentPage = page;
                query.PageSize = 10;
                ImageContentModel model = _siteInfoService.GetSiteImagePageList(query);
                siteModel.ImageList = model.SiteImageContentList;
            }
            return View(siteModel);
        }

        public async Task<ActionResult> VideoList(long ChannelID, int page = 1)
        {
            SiteModel siteModel = new SiteModel();
            siteModel.ConentEntity = new ContentModel();
            string SiteID = SiteConfigManager.SiteID;

            Site_ChannelQuery channelQuery = new Site_ChannelQuery();
            channelQuery.SiteID = SiteID;
            channelQuery.ID = ChannelID;
            Site_Channel channelEntity = await _siteInfoService.GetSiteChannelModel(channelQuery);
            if (channelEntity != null)
            {
                ViewBag.ChannelName = channelEntity.Name;
                ViewBag.ChannelID = channelEntity.ID;
                SiteChannelQuery query = new SiteChannelQuery();
                query.SiteID = SiteConfigManager.SiteID;
                query.ID = ChannelID;
                query.CurrentPage = page;
                query.PageSize = 10;
                IList<SiteChannel_VideoContent> list = _siteInfoService.GetSiteVideoPageList(query);
                siteModel.VideoList = list;
            }
            return View(siteModel);
        }

        public async Task<ActionResult> Show(long ChannelID)
        {
            SiteModel siteModel = new SiteModel();
            siteModel.ConentEntity = new ContentModel();
            string SiteID = SiteConfigManager.SiteID;

            Site_ChannelQuery channelQuery = new Site_ChannelQuery();
            channelQuery.SiteID = SiteID;
            channelQuery.ID = ChannelID;
            Site_Channel channelEntity = await _siteInfoService.GetSiteChannelModel(channelQuery);
            if (channelEntity != null)
            {
                ViewBag.ChannelName = channelEntity.Name;
                ViewBag.ChannelID = channelEntity.ID;

                if (channelEntity.ContentType == 1) //描述类
                {
                    SiteChannelQuery query = new SiteChannelQuery();
                    query.SiteID = SiteID;
                    query.ID = ChannelID;
                    SiteChannel_DescriptionContent model = _siteInfoService.GetDescriptionModel(query);
                    if (model != null)
                    {
                        siteModel.ConentEntity.Title = model.Title;
                        siteModel.ConentEntity.Abstruct = model.Keywords;
                        siteModel.ConentEntity.Content = model.Content.Replace("../Files/", SiteConfigManager.AdminUrl + "/Files/");
                        siteModel.ConentEntity.TitlePhoto = model.TitlePhoto.Replace("/Files/", SiteConfigManager.AdminUrl + "/Files/");
                        siteModel.ConentEntity.AddTime = model.AddTime;
                        siteModel.ConentEntity.LastTime = model.LastTime;
                        siteModel.ConentEntity.Clicks = model.Clicks;
                    }    
                }
                if (channelEntity.ContentType == 2) //新闻资讯类
                {
                    long ItemID = long.Parse(HttpContext.Request.Query["itemid"]);
                    SiteChannelQuery query = new SiteChannelQuery();
                    query.SiteID = SiteID;
                    query.ID = ItemID;
                    SiteChannel_NewsContent model = _siteInfoService.GetNewsModel(query);
                    siteModel.ConentEntity.Title = model.Title;
                    siteModel.ConentEntity.Abstruct = model.Abstruct;
                    siteModel.ConentEntity.Author = model.Author;
                    siteModel.ConentEntity.Source = model.Source;
                    siteModel.ConentEntity.Content = model.Content.Replace("../Files/", SiteConfigManager.AdminUrl + "/Files/");
                    siteModel.ConentEntity.TitlePhoto = model.TitlePhoto.Replace("/Files/", SiteConfigManager.AdminUrl + "/Files/");
                    siteModel.ConentEntity.AddTime = model.AddTime;
                    siteModel.ConentEntity.LastTime = model.LastTime;
                    siteModel.ConentEntity.Clicks = model.Clicks;
                }
                if (channelEntity.ContentType == 3) //图片资讯类
                {
                    long ItemID = long.Parse(HttpContext.Request.Query["itemid"]);
                    SiteChannelQuery query = new SiteChannelQuery();
                    query.SiteID = SiteID;
                    query.ID = ItemID;
                    SiteChannel_ImageContent model = _siteInfoService.GetImageModel(query);
                    siteModel.ConentEntity.Title = model.Title;
                    siteModel.ConentEntity.Abstruct = model.Abstruct;
                    siteModel.ConentEntity.Author = model.Author;
                    siteModel.ConentEntity.Source = model.Source;
                    siteModel.ConentEntity.Content = model.Content.Replace("../Files/", SiteConfigManager.AdminUrl + "/Files/");
                    siteModel.ConentEntity.TitlePhoto = model.ImagePath.Replace("/Files/", SiteConfigManager.AdminUrl + "/Files/");
                    siteModel.ConentEntity.AddTime = model.AddTime;
                    siteModel.ConentEntity.LastTime = model.LastTime;
                    siteModel.ConentEntity.Clicks = model.Clicks;
                }
                if (channelEntity.ContentType == 5) //视频类
                {
                    long ItemID = long.Parse(HttpContext.Request.Query["itemid"]);
                    SiteChannelQuery query = new SiteChannelQuery();
                    query.SiteID = SiteID;
                    query.ID = ItemID;
                    SiteChannel_VideoContent model = _siteInfoService.GetVideoModel(query);
                    siteModel.ConentEntity.Title = model.Title;
                    siteModel.ConentEntity.Abstruct = model.Abstruct;
                    siteModel.ConentEntity.Author = model.Author;
                    siteModel.ConentEntity.Source = model.Source;
                    siteModel.ConentEntity.Content = model.Content.Replace("../Files/", SiteConfigManager.AdminUrl + "/Files/");
                    siteModel.ConentEntity.VideoImage = model.VideoImage.Replace("/Files/", SiteConfigManager.AdminUrl + "/Files/");
                    siteModel.ConentEntity.VideoPath = model.VideoPath.Replace("/Files/", SiteConfigManager.AdminUrl + "/Files/");
                    siteModel.ConentEntity.AddTime = model.AddTime;
                    siteModel.ConentEntity.LastTime = model.LastTime;
                    siteModel.ConentEntity.Clicks = model.Clicks;
                }

            }

            Site_ChannelQuery cq = new Site_ChannelQuery();
            cq.SiteID = SiteID;
            cq.ID = ChannelID;
            ChannelModel m = _siteInfoService.GetChannelList(cq);
            return View(siteModel);

        }

        public async Task<IActionResult> YuDing(int ChannelID,long itemid)
        {
            string SiteID = SiteConfigManager.SiteID;
            Site_ChannelQuery channelQuery = new Site_ChannelQuery();
            channelQuery.SiteID = SiteID;
            channelQuery.ID = ChannelID;
            Site_Channel channelEntity = await _siteInfoService.GetSiteChannelModel(channelQuery);
            if (channelEntity.ContentType == 2)
            {
                SiteChannelQuery query = new SiteChannelQuery();
                query.SiteID = SiteID;
                query.ID = itemid;
                SiteChannel_NewsContent model = _siteInfoService.GetNewsModel(query);
                ViewBag.ContentID = model.ID;
                ViewBag.Content = model.Title;
            }
            if (channelEntity.ContentType == 3)
            {
                SiteChannelQuery query = new SiteChannelQuery();
                query.SiteID = SiteID;
                query.ID = itemid;
                SiteChannel_ImageContent model = _siteInfoService.GetImageModel(query);
                ViewBag.ContentID = model.ID;
                ViewBag.Content = model.Title;
            }
            ViewBag.ChannelName = channelEntity.Name;
            ViewBag.ChannelID = channelEntity.ID;
            return View();
        }

        [HttpPost]
        public IActionResult YuDingAjax(YuDing model)
        {
            ExecResult result = _siteInfoService.SaveYuDing(model);
            return Json(new { code = result.code, msg = result.msg });
        }

    }
}
