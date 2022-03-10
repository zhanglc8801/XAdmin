using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class SiteChannel_FileContent
    {
           public SiteChannel_FileContent(){


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
           /// Desc:所属栏目ID
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
           /// Desc:标题图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TitlePhoto {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FileID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LinkUrl {get;set;}

           /// <summary>
           /// Desc:音频文件大小
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? Size {get;set;}

           /// <summary>
           /// Desc:格式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Format {get;set;}

           /// <summary>
           /// Desc:标题颜色
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TitleColor {get;set;}

           /// <summary>
           /// Desc:是否是粗体 1=是 0=否
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public bool IsBold {get;set;}

           /// <summary>
           /// Desc:是否斜体 1=是 0=否
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public bool? IsItalic {get;set;}

           /// <summary>
           /// Desc:来源
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
           /// Desc:排序
           /// Default:1
           /// Nullable:True
           /// </summary>           
           public int? SortID {get;set;}

           /// <summary>
           /// Desc:状态 1=启用 0=停用
           /// Default:1
           /// Nullable:True
           /// </summary>           
           public bool? Status {get;set;}

           /// <summary>
           /// Desc:添加人
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public long UserID {get;set;}

           /// <summary>
           /// Desc:添加日期
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime AddTime {get;set;}

           /// <summary>
           /// Desc:修改日期
           /// Default:DateTime.Now
           /// Nullable:False
           /// </summary>           
           public DateTime LastTime {get;set;}

           /// <summary>
           /// Desc:点击次数
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public long Clicks {get;set;}

    }
}
