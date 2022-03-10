using System;
namespace XAdmin.Model.Entity
{
    ///<summary>
    ///作者信息
    ///</summary>
    public partial class AuthorInfo
    {
           public AuthorInfo(){


           }
           /// <summary>
           /// Desc:作者信息
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long AuthorID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long JournalID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LoginName {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Pwd {get;set;}

           /// <summary>
           /// Desc:真实姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string RealName {get;set;}

           /// <summary>
           /// Desc:手机号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Mobile {get;set;}

           /// <summary>
           /// Desc:登录ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LoginIP {get;set;}

           /// <summary>
           /// Desc:登录次数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int LoginCount {get;set;}

           /// <summary>
           /// Desc:登录时间
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime LoginDate {get;set;}

           /// <summary>
           /// Desc:所属分组 1=编辑 2=作者 3=专家
           /// Default:2
           /// Nullable:False
           /// </summary>           
           public byte GroupID {get;set;}

           /// <summary>
           /// Desc:状态 1=正常 0=删除
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public byte Status {get;set;}

           /// <summary>
           /// Desc:积分
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int Points {get;set;}

           /// <summary>
           /// Desc:
           /// Default:-1
           /// Nullable:False
           /// </summary>           
           public int IsAuth {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime AddDate {get;set;}

    }
}
