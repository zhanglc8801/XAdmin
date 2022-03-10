using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class SiteChannel_ImageContent
    {
        public SiteChannel_ImageContent()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ChannelID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:摘要
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Abstruct { get; set; }

        /// <summary>
        /// Desc:图片路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ImagePath { get; set; }

        /// <summary>
        /// 附件路径
        /// </summary>
        public string FilePath { get; set; }

        public string Content { get; set; }

        /// <summary>
        /// Desc:外链
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LinkUrl { get; set; }

        /// <summary>
        /// Desc:是否在新窗口打开
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public bool? Target { get; set; }

        /// <summary>
        /// Desc:来源
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Source { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Author { get; set; }

        /// <summary>
        /// Desc:标签
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Tags { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public int? SortID { get; set; }

        /// <summary>
        /// Desc:状态 1=启用 0=停用
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:添加人
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public long UserID { get; set; }

        /// <summary>
        /// Desc:添加日期
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// Desc:修改日期
        /// Default:DateTime.Now
        /// Nullable:False
        /// </summary>           
        public DateTime? LastTime { get; set; }

        /// <summary>
        /// Desc:点击次数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public long Clicks { get; set; }

    }
}
