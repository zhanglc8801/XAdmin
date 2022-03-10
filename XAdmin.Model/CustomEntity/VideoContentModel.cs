using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Model.Entity;

namespace XAdmin.Model.CustomEntity
{
    public class VideoContentModel: PageBase
    {
        public List<SiteChannel_VideoContentEx> VideoContentList { get; set; }
    }
    public class SiteChannel_VideoContentEx : SiteChannel_VideoContent
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
                return AddTime == null ? "1970-01-01 12:00" : AddTime.ToString("yyyy-MM-dd HH:mm");
            }
        }

        public string LastTimeStr
        {
            get
            {
                return LastTime == null ? "1970-01-01 12:00" : LastTime.ToString("yyyy-MM-dd HH:mm");
            }
        }
    }
}
