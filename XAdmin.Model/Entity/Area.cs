using System;
using System.Linq;
using System.Text;

namespace XAdmin.Model.Entity
{
    ///<summary>
    ///
    ///</summary>
    public partial class Area
    {
        public Area()
        {


        }
        /// <summary>
        /// Desc:ID主键值
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ID { get; set; }

        /// <summary>
        /// Desc:区域编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AreaCode { get; set; }

        /// <summary>
        /// Desc:区域名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AreaName { get; set; }

        /// <summary>
        /// Desc:父级ID值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParentID { get; set; }

        /// <summary>
        /// Desc:行政区划级别：0:国家，1:省直辖市，2:地区级市，3:区县
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? AreaLevel { get; set; }

        /// <summary>
        /// Desc:顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? SNumber { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Remarks { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ShortName { get; set; }

        /// <summary>
        /// Desc:状态 1=启用 0=停用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Status { get; set; }

    }
}
