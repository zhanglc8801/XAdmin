using System;
namespace XAdmin.Model.QueryEntity
{
    public class InitSiteInfoQuery
    {
        /// <summary>
        /// 站点ID
        /// </summary>
        public string SiteID { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Desc:站点英文名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteNameEn { get; set; }

        /// <summary>
        /// Desc:站点Url
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteUrl { get; set; }

        /// <summary>
        /// Desc:关键字
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteKey { get; set; }

        /// <summary>
        /// Desc:描述信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteDes { get; set; }

        /// <summary>
        /// Desc:ICP备案号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteICP { get; set; }

        /// <summary>
        /// Desc:发件名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SendMailName { get; set; }

        /// <summary>
        /// Desc:邮件服务器地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MailServer { get; set; }

        /// <summary>
        /// Desc:邮件服务器端口号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? MailPort { get; set; }

        /// <summary>
        /// Desc:邮件帐户
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MailAccount { get; set; }

        /// <summary>
        /// Desc:邮件密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MailPwd { get; set; }

        /// <summary>
        /// Desc:是否开启SSL
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? MailSSL { get; set; }

        /// <summary>
        /// Desc:短信服务地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SMSUrl { get; set; }

        /// <summary>
        /// Desc:短信服务用户名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SMSUser { get; set; }

        /// <summary>
        /// Desc:短信服务密码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SMSPwd { get; set; }

        /// <summary>
        /// Desc:短信前缀(签名)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SMSPrefix { get; set; }

        /// <summary>
        /// Desc:创建日期
        /// Default:DateTime.Now
        /// Nullable:True
        /// </summary>           
        public DateTime? AddDate { get; set; }

        //=====================================

        public string UserName { get; set; }

        public string Pwd { get; set; }



    }
}
