using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class RoleMenu
    {
           public RoleMenu(){


           }
           /// <summary>
           /// Desc:角色菜单ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RMID {get;set;}

           /// <summary>
           /// Desc:站点ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteID {get;set;}

           /// <summary>
           /// Desc:角色ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? RoleID {get;set;}

           /// <summary>
           /// Desc:菜单ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? MenuID {get;set;}

        public string AuthStr { get; set; }

           /// <summary>
           /// Desc:创建日期
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? AddDate {get;set;}

    }
}
