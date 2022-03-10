using XAdmin.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    public class RoleInfoListModel: PageBase
    {
        public List<RoleInfo> RoleInfoList { get; set; }
    }
}
