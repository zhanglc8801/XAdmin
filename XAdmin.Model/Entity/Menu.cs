using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class Menu
    {
           public Menu(){


           }
           /// <summary>
           /// Desc:菜单ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int MenuID {get;set;}

           /// <summary>
           /// Desc:站点ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteID {get;set;}

           /// <summary>
           /// Desc:父菜单ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? PMenuID {get;set;}

           /// <summary>
           /// Desc:菜单名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MenuName {get;set;}

           /// <summary>
           /// Desc:菜单URL
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MenuUrl {get;set;}

           /// <summary>
           /// Desc:菜单图标，以unicode编码的iconfont
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Icon {get;set;}

           /// <summary>
           /// Desc:图标颜色
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IconColor {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? SortID {get;set;}

           /// <summary>
           /// Desc:状态 1=启用 0=停用
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? Status {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? AddDate {get;set;}

    }
}
