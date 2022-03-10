using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class RoleInfo
    {
           public RoleInfo(){


           }
           /// <summary>
           /// Desc:角色信息表，主键
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
           /// Desc:角色名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RoleName {get;set;}

           /// <summary>
           /// Desc:角色描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Note {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? AddDate {get;set;}

    }
}
