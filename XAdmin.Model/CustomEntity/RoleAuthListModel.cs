using XAdmin.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    public class RoleAuthListModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Note { get; set; }

        public string MenuIDStr { get; set; }
        public string MenuAuthStr { get; set; }

        public List<RoleMenu> RoleAuthList { get; set; }
    }
}
