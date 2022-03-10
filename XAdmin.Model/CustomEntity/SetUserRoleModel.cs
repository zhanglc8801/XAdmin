using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    public class SetUserRoleModel
    {
        public string SiteID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 以逗号分隔的角色ID字符串
        /// </summary>
        public string RoleIdStr { get; set; }
    }
}
