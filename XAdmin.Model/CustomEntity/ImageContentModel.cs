using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Model.Entity;

namespace XAdmin.Model.CustomEntity
{
    /// <summary>
    /// 图片类资讯分页实体
    /// </summary>
    public class ImageContentModel: PageBase
    {
        public List<SiteChannel_ImageContentEx> ImageContentList { get; set; }
        public List<SiteChannel_ImageContent> SiteImageContentList { get; set; }
    }
    public class SiteChannel_ImageContentEx : SiteChannel_ImageContent
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
