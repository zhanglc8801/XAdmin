using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class SiteChannel_NewsContent
    {
           public SiteChannel_NewsContent(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long ID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SiteID {get;set;}

           /// <summary>
           /// Desc:栏目ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? ChannelID {get;set;}

           /// <summary>
           /// Desc:标题
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:标题颜色
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TitleColor {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FileIDStr {get;set;}

           /// <summary>
           /// Desc:是否粗体
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? IsBold {get;set;}

           /// <summary>
           /// Desc:是否斜体
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? IsItalic {get;set;}

           /// <summary>
           /// Desc:外链地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LinkUrl {get;set;}

           /// <summary>
           /// Desc:是否在新窗口打开
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? Target {get;set;}

           /// <summary>
           /// Desc:新闻来源
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Source {get;set;}

           /// <summary>
           /// Desc:作者
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Author {get;set;}

           /// <summary>
           /// Desc:标签
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tags {get;set;}

           /// <summary>
           /// Desc:摘要
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Abstruct {get;set;}

           /// <summary>
           /// Desc:标题图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TitlePhoto {get;set;}

           /// <summary>
           /// Desc:内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Content {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? UserId {get;set;}

           /// <summary>
           /// Desc:状态 1=启用 0=停用
           /// Default:1
           /// Nullable:True
           /// </summary>           
           public bool? Status {get;set;}

           /// <summary>
           /// Desc:添加时间
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? AddTime {get;set;}

           /// <summary>
           /// Desc:最后修改时间
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? LastTime {get;set;}

           /// <summary>
           /// Desc:点击次数
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public long? Clicks {get;set;}

    }
}
