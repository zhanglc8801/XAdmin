using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class UserDetail
    {
           public UserDetail(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long PKID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? UserID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteID {get;set;}

           /// <summary>
           /// Desc:所在省份
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Province {get;set;}

           /// <summary>
           /// Desc:所在城市
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string City {get;set;}

           /// <summary>
           /// Desc:所在区县
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string County {get;set;}

           /// <summary>
           /// Desc:民族
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Nation {get;set;}

           /// <summary>
           /// Desc:出生日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? Birthday {get;set;}

           /// <summary>
           /// Desc:证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? IDType {get;set;}

           /// <summary>
           /// Desc:证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IDNo {get;set;}

           /// <summary>
           /// Desc:详细地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:邮编
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZipCode {get;set;}

           /// <summary>
           /// Desc:工作单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WorkUnit {get;set;}

           /// <summary>
           /// Desc:单位电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tel {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Fox {get;set;}

           /// <summary>
           /// Desc:学历
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? Education {get;set;}

           /// <summary>
           /// Desc:毕业院校
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string University {get;set;}

           /// <summary>
           /// Desc:专业
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Professional {get;set;}

           /// <summary>
           /// Desc:职称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JobTitle {get;set;}

    }
}
