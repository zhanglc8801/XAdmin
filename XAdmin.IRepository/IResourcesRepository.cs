using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XAdmin.IRepository
{
    /// <summary>
    /// 资源类接口
    /// </summary>
    public interface IResourcesRepository
    {
        /// <summary>
        /// 插入资源表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult InsertResources(Resources model);

        /// <summary>
        /// 获取所有资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ResourcesModel> GetAllResourcesPageList(ResourcesQuery query);

        /// <summary>
        /// 根据条件获取资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ResourcesModel> GetResourcesPageList(ResourcesQuery query);
        
        /// <summary>
        /// 根据ID字符串获取文件列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<Resources>> GetResourcesList(ResourcesQuery query);
        
        /// <summary>
        /// 根据文件ID获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Resources GetResourcesModel(ResourcesQuery query);
        
        /// <summary>
        /// 根据文件MD5获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Resources GetResourcesModelByMD5(ResourcesQuery query);
        
    }
}
