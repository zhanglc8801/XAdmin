using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace XAdmin.Common
{
    public class SiteConfigManager
    {
        public readonly static IConfiguration Configuration;
        static SiteConfigManager()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/Config/")
                .AddJsonFile("SiteConfig.json", optional: true)
                .Build();
        }

        /// <summary>
        /// 获得配置文件的对象值
        /// </summary>
        /// <param name="jsonPath">json文件地址</param>
        /// <param name="key">json某个对象</param>
        /// <returns></returns>
        public static string GetBasicConfigJson(string key)
        {
            return Configuration.GetSection("BasicConfig").GetSection(key).Value;
        }

        /// <summary>
        /// 获得配置文件的对象值
        /// </summary>
        /// <param name="jsonPath">json文件地址</param>
        /// <param name="key">json某个对象</param>
        /// <returns></returns>
        public static string GetMoreConfigJson(string key)
        {
            return Configuration.GetSection("MoreConfig").GetSection(key).Value;
        }

        /// <summary>
        /// 获取配置文件中的SiteID
        /// </summary>
        public static string SiteID
        {
            get { return GetBasicConfigJson("SiteID"); }
        }

        /// <summary>
        /// 获取配置文件中的SiteName
        /// </summary>
        public static string SiteName
        {
            get { return GetBasicConfigJson("SiteName"); }
        }

        /// <summary>
        /// 获取配置文件中的SiteDomain
        /// </summary>
        public static string SiteDomain
        {
            get { return GetBasicConfigJson("SiteDomain"); }
        }

        /// <summary>
        /// 站点根目录
        /// </summary>
        public static string RootPath
        {
            get { return GetBasicConfigJson("RootPath"); }
        }

        /// <summary>
        /// API地址
        /// </summary>
        public static string APIHost
        {
            get { return GetBasicConfigJson("APIHost"); }
        }

        /// <summary>
        /// 后台管理地址
        /// </summary>
        public static string AdminUrl
        {
            get { return GetBasicConfigJson("AdminUrl"); }
        }

        /// <summary>
        /// 上传文件保存路径
        /// </summary>
        public static string UploadPath
        {
            get { return GetBasicConfigJson("UploadPath"); }
        }

        /// <summary>
        /// 允许上传的文件类型
        /// </summary>
        public static string UploadFileExt
        {
            get { return GetBasicConfigJson("UploadFileExt"); }
        }

        /// <summary>
        /// 头像保存路径
        /// </summary>
        public static string HeadPhotoPath
        {
            get { return GetBasicConfigJson("HeadPhotoPath"); }
        }

        /// <summary>
        /// 视频保存路径
        /// </summary>
        public static string VideoPath
        {
            get { return GetBasicConfigJson("VideoPath"); }
        }

        //================================

        /// <summary>
        /// 站点关键字
        /// </summary>
        public static string SiteKeywords
        {
            get { return GetMoreConfigJson("SiteKeywords"); }
        }

        /// <summary>
        /// 描述信息
        /// </summary>
        public static string SiteDescription
        {
            get { return GetMoreConfigJson("SiteDescription"); }
        }

        /// <summary>
        /// 页面底部版权信息
        /// </summary>
        public static string SiteCopyright
        {
            get { return GetMoreConfigJson("SiteCopyright"); }
        }

        /// <summary>
        /// ICP备案号
        /// </summary>
        public static string ICP
        {
            get { return GetMoreConfigJson("ICP"); }
        }

        /// <summary>
        /// 统计代码
        /// </summary>
        public static string StatisticCode
        {
            get { return GetMoreConfigJson("StatisticCode"); }
        }

        /// <summary>
        /// 最大允许登录错误次数
        /// </summary>
        public static string MaxLoginErrCount
        {
            get { return GetMoreConfigJson("MaxLoginErrCount"); }
        }

        /// <summary>
        /// 锁定时长
        /// </summary>
        public static string LockTime
        {
            get { return GetMoreConfigJson("LockTime"); }
        }


    }
}
