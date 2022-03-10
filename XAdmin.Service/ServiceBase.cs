using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Common;

namespace XAdmin.Service
{
    public class ServiceBase
    {
        /// <summary>
        /// 获取API地址
        /// </summary>
        /// <param name="ActionUrl"></param>
        /// <returns></returns>
        public string GetAPIUrl(string ActionUrl)
        {
            return SiteConfigManager.APIHost + ActionUrl;
        }
    }
}
