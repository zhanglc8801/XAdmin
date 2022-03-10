using XAdmin.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    public class AreaModel:PageBase
    {
        public List<AreaEx> AreaList { get; set; }
    }

    public class AreaEx : Area
    { 
        public string AreaLevelStr {
            get {
                if (AreaLevel == 0)
                    return "国家级";
                else if (AreaLevel == 1)
                    return "省级";
                else if (AreaLevel == 2)
                    return "市级";
                else if (AreaLevel == 3)
                    return "县级";
                else
                    return "未知";
            }
        }

        public string StatusStr
        {
            get
            {
                if (Status == true)
                    return "正常";
                else
                    return "已停用";
            }
        }

    }

}
