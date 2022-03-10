using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;

namespace XAdmin.IRepository
{
    public interface ISiteSetRepository
    {
        /// <summary>
        /// 初始化站点配置
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        bool InitSiteInfo(InitSiteInfoQuery query);

        /// <summary>
        /// 获取站点配置信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        SiteInfo GetSiteInfoModel(SiteInfoQuery query);

        /// <summary>
        /// 保存站点配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveSiteInfo(SiteInfo model);
    }
}
