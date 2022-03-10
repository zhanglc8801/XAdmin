using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.Common;
using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;

namespace Web.Site
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 后台管理地址
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string AdminUrl(this IHtmlHelper helper)
        {
            return SiteConfigManager.AdminUrl;
        }

        /// <summary>
        /// 截取固定长度的字符串
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(this IHtmlHelper helper, string str, int length)
        {
            return TextHelper.SubStr(str, length);
        }

        /// <summary>
        /// 截取固定长度的字符串 并使用指定字符串代替超出的部分
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string CutString(this IHtmlHelper helper, string str, int length, string suffix)
        {
            return TextHelper.SubStr(str, length, suffix);
        }

        /// <summary>
        /// 获取描述类内容
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ChannelID"></param>
        /// <returns></returns>
        public static SiteChannel_DescriptionContent GetDescription(this IHtmlHelper helper,int ChannelID)
        {
            ISiteInfoManagementService svc=AutofacUtil.GetScopeService<ISiteInfoManagementService>();
            SiteChannelQuery query = new SiteChannelQuery();
            query.ID = ChannelID;
            query.SiteID = SiteConfigManager.SiteID;
            SiteChannel_DescriptionContent model = svc.GetDescriptionModel(query);
            return model;
        }

        /// <summary>
        /// 获取新闻资讯类数据
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ChannelID"></param>
        /// <param name="TopCount"></param>
        /// <returns></returns>
        public static IList<SiteChannel_NewsContent> GetSiteNewsPageList(this IHtmlHelper helper, long ChannelID, int TopCount)
        {
            ISiteInfoManagementService svc = AutofacUtil.GetScopeService<ISiteInfoManagementService>();
            SiteChannelQuery query = new SiteChannelQuery();
            query.SiteID = SiteConfigManager.SiteID;
            query.ID = ChannelID;
            query.CurrentPage = 1;
            query.PageSize = TopCount;
            IList<SiteChannel_NewsContent> list = svc.GetSiteNewsPageList(query);
            return list;
        }

        /// <summary>
        /// 获取图片类资讯
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="ChannelID"></param>
        /// <param name="TopCount"></param>
        /// <returns></returns>
        public static ImageContentModel GetSiteImagePageList(this IHtmlHelper helper, long ChannelID, int TopCount)
        {
            ISiteInfoManagementService svc = AutofacUtil.GetScopeService<ISiteInfoManagementService>();
            SiteChannelQuery query = new SiteChannelQuery();
            query.SiteID = SiteConfigManager.SiteID;
            query.ID = ChannelID;
            query.CurrentPage = 1;
            query.PageSize = TopCount;
            ImageContentModel model = svc.GetSiteImagePageList(query);
            return model;
        }

    }
}
