using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.QueryEntity
{
    public class MenuIconQuery:QueryBase
    {
        /// <summary>
        /// Desc:图标ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int ID { get; set; }

        /// <summary>
        /// Desc:图标名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IconName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? Sort { get; set; }

        /// <summary>
        /// Desc:图标状态 1=启用 0=禁用
        /// Default:
        /// Nullable:True
        /// </summary>           
        public bool? Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Notes { get; set; }
    }
}
