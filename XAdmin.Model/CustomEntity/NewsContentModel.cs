using XAdmin.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace XAdmin.Model.CustomEntity
{
    public class NewsContentModel: PageBase
    {
        public List<SiteChannel_NewsContentEx> NewsContentList { get; set; }
    }
    public class SiteChannel_NewsContentEx : SiteChannel_NewsContent
    {
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }

        public string AddTimeStr
        {
            get
            {
                return AddTime == null ? "1970-01-01 12:00" : AddTime.Value.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public string LastTimeStr
        {
            get
            {
                return LastTime == null ? "1970-01-01 12:00" : LastTime.Value.ToString("yyyy-MM-dd HH:mm");
            }
        }
    }
}
