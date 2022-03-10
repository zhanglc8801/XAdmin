using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Service
{
    public class ResourcesService : ServiceBase,IResourcesService
    {
        /// <summary>
        /// 插入资源表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult InsertResources(Resources model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, Resources>(GetAPIUrl(APIConstants.InsertResources), model);
            return result;
        }

        /// <summary>
        /// 获取所有资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ResourcesModel> GetAllResourcesPageList(ResourcesQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ResourcesModel model = await clientHelper.PostAuthAsync<ResourcesModel, ResourcesQuery>(GetAPIUrl(APIConstants.GetAllResourcesPageList), query);
            return model;
        }

        /// <summary>
        /// 根据条件获取资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ResourcesModel> GetResourcesPageList(ResourcesQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ResourcesModel model = await clientHelper.PostAuthAsync<ResourcesModel, ResourcesQuery>(GetAPIUrl(APIConstants.GetResourcesPageList), query);
            return model;
        }

        /// <summary>
        /// 根据ID字符串获取文件列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Resources>> GetResourcesList(ResourcesQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<Resources> list = await clientHelper.PostAuthAsync<List<Resources>, ResourcesQuery>(GetAPIUrl(APIConstants.GetResourcesList), query);
            return list;
        }

        /// <summary>
        /// 根据文件ID获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Resources GetResourcesModel(ResourcesQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            Resources resources = clientHelper.PostAuth<Resources, ResourcesQuery>(GetAPIUrl(APIConstants.GetResourcesModel), query);
            return resources;
        }

        /// <summary>
        /// 根据文件MD5获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Resources GetResourcesModelByMD5(ResourcesQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            Resources resources = clientHelper.PostAuth<Resources, ResourcesQuery>(GetAPIUrl(APIConstants.GetResourcesModelByMD5), query);
            return resources;
        }
    }
}
