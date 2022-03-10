using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class Site_Channel
    {
        public Site_Channel()
        {
            this.SortID = 1;
        }
        /// <summary>
        /// Desc:栏目ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        
        public int ID { get; set; }

        /// <summary>
        /// Desc:栏目名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Name { get; set; }

        /// <summary>
        /// Desc:上级栏目ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParentID { get; set; }

        /// <summary>
        /// Desc:站点ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SiteID { get; set; }

        /// <summary>
        /// Desc:允许管理的角色ID字符串
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RoleIDs { get; set; }

        /// <summary>
        /// Desc:允许管理的角色名称字符串
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RoleNames { get; set; }

        /// <summary>
        /// Desc:是否在导航栏显示
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? IsNav { get; set; }

        /// <summary>
        /// Desc:内容类型 0=仅栏目 1=描述类 2=列表类 3=图片类 4=音频类 5=视频类
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? ContentType { get; set; }

        /// <summary>
        /// Desc:关键词
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Keywords { get; set; }

        /// <summary>
        /// Desc:简介
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Description { get; set; }

        /// <summary>
        /// Desc:排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? SortID { get; set; }

        /// <summary>
        /// Desc:状态 1=启用 0=禁用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// Desc:添加日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? AddDate { get; set; }

    }
}
