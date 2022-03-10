using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class DesContent
    {
           public DesContent(){


           }
           /// <summary>
           /// Desc:描述类内容，主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long DesID {get;set;}

           /// <summary>
           /// Desc:站点ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteID {get;set;}

           /// <summary>
           /// Desc:栏目ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? ChannelID {get;set;}

           /// <summary>
           /// Desc:标题
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Description {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? AddDate {get;set;}

    }
}
