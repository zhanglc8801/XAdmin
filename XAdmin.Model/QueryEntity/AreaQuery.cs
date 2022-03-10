using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.QueryEntity
{
    public class AreaQuery:QueryBase
    {
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

        ///// <summary>
        ///// 查询单位级别 0=测绘单位 1=国家测绘局 2=省级管理部门 3=市级管理部门 4=县级管理部门
        ///// </summary>
        //public int UnitLevel { get; set; }
    }
}
