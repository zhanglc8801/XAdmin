using XAdmin.Common;
using XAdmin.Common.Security;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace Web.Admin
{
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// 当前登录人信息
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string CurUserLoginName(this IHtmlHelper helper)
        {
            //if (TicketTool.IsLogin())
            //{
            //    var LoginAuthor = JsonConvert.DeserializeObject<UserInfo>(TicketTool.GetUserData());
            //    return LoginAuthor.UserName;
            //}

            return "";
        }

        /// <summary>
        /// 站点ID
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string SiteID(this IHtmlHelper helper)
        {
            return SiteConfigManager.SiteID;
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string SiteName(this IHtmlHelper helper)
        {
            return SiteConfigManager.SiteName;
        }

        /// <summary>
        /// 站点首页
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string SiteHome(this IHtmlHelper helper)
        {
            return XAdmin.Common.Utils.PublicMethod.SiteHome();
        }

        /// <summary>
        /// 资源根路径
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string RootPath(this IHtmlHelper helper)
        {
            return SiteConfigManager.RootPath;
        }

        /// <summary>
        /// 上传文件格式
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string UploadFileExt(this IHtmlHelper helper)
        {
            return SiteConfigManager.UploadFileExt;
        }

        /// <summary>
        /// 上传路径
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string UploadPath(this IHtmlHelper helper)
        {
            return SiteConfigManager.UploadPath;
        }

        /// <summary>
        /// 站点关键字
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string SiteKeywords(this IHtmlHelper helper)
        {
            return SiteConfigManager.SiteKeywords;
        }

        /// <summary>
        /// 站点描述
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static string SiteDescription(this IHtmlHelper helper)
        {
            return SiteConfigManager.SiteDescription;
        }

        /// <summary>
        /// ICP备案号
        /// </summary>
        public static string SiteICP(this IHtmlHelper helper)
        {
            return SiteConfigManager.ICP;
        }

        /// <summary>
        /// 统计代码
        /// </summary>
        public static string StatisticCode(this IHtmlHelper helper)
        {
            return SiteConfigManager.StatisticCode;
        }

        /// <summary>
        /// 获取截取内容
        /// </summary>
        /// <param name="board"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubHtmlText(this IHtmlHelper helper, string htmlStr, Int32 length)
        {
            string text = string.Empty;
            if (!string.IsNullOrWhiteSpace(htmlStr))
            {
                text = Regex.Replace(htmlStr, @"(<[^>]*>)|([\s　])", string.Empty, RegexOptions.IgnoreCase);
                text = text.Replace("\n", "").Replace("\t", "");
                if (text.Length > length)
                    text = text.Substring(0, length);
            }
            return text;
        }

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string UrlEncode(this IHtmlHelper helper, string param)
        {
            return HttpUtility.UrlEncode(param);
        }

        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string UrlDecode(this IHtmlHelper helper, string param)
        {
            return HttpUtility.UrlDecode(param);
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(this IHtmlHelper helper, string filePath)
        {
            return Path.GetFileName(filePath);
        }

    }
}
