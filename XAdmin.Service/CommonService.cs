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
    public class CommonService : ServiceBase, ICommonService
    {
        /// <summary>
        /// 获取省级行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetProvinceList(AreaQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<Area> list = await clientHelper.PostAuthAsync<List<Area>, AreaQuery>(GetAPIUrl(APIConstants.GetProvinceList), query);
            return list;
        }
        /// <summary>
        /// 根据ParentID获取行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetAreaListByParentID(AreaQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<Area> list = await clientHelper.PostAuthAsync<List<Area>, AreaQuery>(GetAPIUrl(APIConstants.GetAreaListByParentID), query);
            return list;
        }

        /// <summary>
        /// 获取行政区划实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Area GetAreaModel(AreaQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            Area model = clientHelper.PostAuth<Area, AreaQuery>(GetAPIUrl(APIConstants.GetAreaModel), query);
            return model;
        }

        /// <summary>
        /// 通用选择区域下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeSelectModel> MakeAreaTreeSelect(AreaQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            TreeSelectModel model = await clientHelper.PostAuthAsync<TreeSelectModel, QueryBase>(GetAPIUrl(APIConstants.MakeAreaTreeSelect), query);
            return model;
        }
    }
}
