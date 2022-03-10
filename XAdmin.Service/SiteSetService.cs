using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Model.CustomEntity;

namespace XAdmin.Service
{
    public class SiteSetService: ServiceBase,ISiteSetService
    {
        /// <summary>
        /// 初始化站点信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool InitSiteInfo(InitSiteInfoQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            bool result = clientHelper.PostAuth<bool, InitSiteInfoQuery>(GetAPIUrl(APIConstants.InitSiteInfo), query);
            return result;
        }

        /// <summary>
        /// 获取站点配置信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteInfo GetSiteInfoModel(SiteInfoQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            SiteInfo result = clientHelper.PostAuth<SiteInfo, SiteInfoQuery>(GetAPIUrl(APIConstants.GetSiteInfo), query);
            return result;
        }

        /// <summary>
        /// 保存站点配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveSiteInfo(SiteInfo model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, SiteInfo>(GetAPIUrl(APIConstants.SaveSiteInfo), model);
            return result;
        }
    }
}
