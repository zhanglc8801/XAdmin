using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XAdmin.IService
{
    public interface ICommonService
    {
        /// <summary>
        /// 获取省级行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<Area>> GetProvinceList(AreaQuery query);

        /// <summary>
        /// 根据ParentID获取行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<Area>> GetAreaListByParentID(AreaQuery query);

        /// <summary>
        /// 获取行政区划实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Area GetAreaModel(AreaQuery query);

        /// <summary>
        /// 通用选择区域下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TreeSelectModel> MakeAreaTreeSelect(AreaQuery query);
    }
}
