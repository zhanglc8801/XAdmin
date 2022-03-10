using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class Form
    {
           public Form(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ID {get;set;}

           /// <summary>
           /// Desc:表单名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FormName {get;set;}

           /// <summary>
           /// Desc:表达类别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? FormType {get;set;}

           /// <summary>
           /// Desc:使用用户ID列表
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserList {get;set;}

           /// <summary>
           /// Desc:修改用户ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? UserId {get;set;}

           /// <summary>
           /// Desc:修改用户名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ContentStr {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ContentHead {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TableExChange {get;set;}

           /// <summary>
           /// Desc:是否开启
           /// Default:1
           /// Nullable:True
           /// </summary>           
           public bool? IsOpen {get;set;}

           /// <summary>
           /// Desc:添加日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? AddDate {get;set;}

    }
}
