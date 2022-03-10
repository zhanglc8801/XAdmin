using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class SiteInfo
    {
           public SiteInfo(){


           }
           /// <summary>
           /// Desc:站点信息表，主键
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ID {get;set;}

           /// <summary>
           /// Desc:站点ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteID {get;set;}

           /// <summary>
           /// Desc:站点名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteName {get;set;}

           /// <summary>
           /// Desc:站点Url
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteUrl {get;set;}

           /// <summary>
           /// Desc:发件名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SendMailName {get;set;}

           /// <summary>
           /// Desc:邮件服务器地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MailServer {get;set;}

           /// <summary>
           /// Desc:邮件服务器端口号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MailPort {get;set;}

           /// <summary>
           /// Desc:邮件帐户
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MailAccount {get;set;}

           /// <summary>
           /// Desc:邮件密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MailPwd {get;set;}

           /// <summary>
           /// Desc:是否开启SSL
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? MailSSL {get;set;}

           /// <summary>
           /// Desc:短信服务地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SMSUrl {get;set;}

           /// <summary>
           /// Desc:短信服务用户名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SMSUser {get;set;}

           /// <summary>
           /// Desc:短信服务密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SMSPwd {get;set;}

           /// <summary>
           /// Desc:短信前缀(签名)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SMSPrefix {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? UserID {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? AddDate {get;set;}

    }
}
