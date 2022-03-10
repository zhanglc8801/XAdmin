using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Model.CustomEntity
{
    public class ChannelModel
    {
        /// <summary>
        /// Desc:栏目ID
        /// Default:
        /// Nullable:False
        /// </summary>           

        public long ID { get; set; }

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
        /// Desc:内容类型 0=仅栏目 1=描述类 2=列表类 3=图片类 4=音频类 5=视频类
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte? ContentType { get; set; }

        public List<ChannelModel> ParentList { get; set; }

    }
}
