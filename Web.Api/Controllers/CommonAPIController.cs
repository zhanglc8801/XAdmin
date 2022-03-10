using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Controllers
{
    /// <summary>
    /// 公共操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonAPIController : ControllerBase
    {
        private ICommonRepository _commonRepository;
        public CommonAPIController(ICommonRepository commonRepository)
        {
            this._commonRepository = commonRepository;
        }

        /// <summary>
        /// 获取省级行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Area>> GetProvinceList(AreaQuery query)
        {
            return await _commonRepository.GetProvinceList(query);
        }

        /// <summary>
        /// 根据ParentID获取行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Area>> GetAreaListByParentID(AreaQuery query)
        {
            return await _commonRepository.GetAreaListByParentID(query);
        }

        /// <summary>
        /// 获取行政区划实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public Area GetAreaModel(AreaQuery query)
        {
            return _commonRepository.GetAreaModel(query);
        }

        /// <summary>
        /// 通用选择区域下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TreeSelectModel> MakeAreaTreeSelect(AreaQuery query)
        {
            return await _commonRepository.MakeAreaTreeSelect(query);
        }
    }
}
