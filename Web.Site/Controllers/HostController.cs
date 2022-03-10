using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class HostController : Controller
    {
        private readonly ISiteInfoManagementService _siteInfoService;
        public HostController(ISiteInfoManagementService siteInfoService, IResourcesService resService)
        {
            this._siteInfoService = siteInfoService;
        }

        public async Task<IActionResult> Index(long ChannelID=16, int page = 1)
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
            ViewBag.ChannelID = ChannelID;
            return View(siteModel);
        }

        [HttpPost]
        public IActionResult GetImagePageList(int page, int limit, SiteChannelQuery query)
        {
            query.SiteID = SiteConfigManager.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            ImageContentModel model = _siteInfoService.GetSiteImagePageList(query);
            if (model != null && model.SiteImageContentList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, list = model.SiteImageContentList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "没有获取到符合条件的数据！" });
        }


    }
}
