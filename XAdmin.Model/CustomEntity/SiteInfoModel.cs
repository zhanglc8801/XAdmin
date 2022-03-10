using XAdmin.Model.Entity;
namespace XAdmin.Model.CustomEntity
{
    public class SiteInfoModel:SiteInfo
    {
        /// <summary>
        /// 允许上传文件的格式
        /// </summary>
        public string UploadFileExt { get; set; }

        /// <summary>
        /// 上传虚拟路径
        /// </summary>
        public string UploadPath { get; set; }

        /// <summary>
        /// API授权KEY
        /// </summary>
        public string APIPublicKey { get; set; }

        /// <summary>
        /// API地址
        /// </summary>
        public string APIHost { get; set; }

        /// <summary>
        /// 站点关键字
        /// </summary>
        public string SiteKeywords { get; set; }

        /// <summary>
        /// 站点描述
        /// </summary>
        public string SiteDescription { get; set; }

        /// <summary>
        /// 底部版权信息
        /// </summary>
        public string SiteCopyright { get; set; }

        /// <summary>
        /// ICP备案号
        /// </summary>
        public string SiteICP { get; set; }

        /// <summary>
        /// 超级管理员帐户
        /// </summary>
        public string SysSuperAdmin { get; set; }

        /// <summary>
        /// 统计代码
        /// </summary>
        public string StatisticCode { get; set; }

        /// <summary>
        /// 允许访问的主机类型 1=IP 2=计算机名 0=不限制
        /// </summary>
        public string AccessHostType { get; set; }

        /// <summary>
        /// 允许访问的主机/IP列表
        /// </summary>
        public string AccessHostList { get; set; }

        /// <summary>
        /// 后台最大登录失败次数
        /// </summary>
        public string MaxLoginErrCount { get; set; }

        /// <summary>
        /// 达到最大失败次数后的锁定时间
        /// </summary>
        public string LockTime { get; set; }

        /// <summary>
        /// 启用注册激活 0:不启用 1:启用
        /// </summary>
        public string EnableRegActivate { get; set; }
    }
}
