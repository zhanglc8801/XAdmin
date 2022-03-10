using XAdmin.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    public class GroupInfoListModel : PageBase
    {
        public List<GroupInfo> GroupInfoList { get; set; }
    }
}
