using XAdmin.IService;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Admin.Controllers
{
    public class CommonController : BaseController
    {
        private readonly ICommonService _comService;
        public CommonController(ICommonService comService)
        {
            this._comService = comService;
        }

        /// <summary>
        /// 获取省级行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetProvinceList(AreaQuery query)
        {
            List<Area> list = new List<Area>();
            list = await _comService.GetProvinceList(query);
            return Json(new { code = 1, data = list, msg = "Success" });
        }

        /// <summary>
        /// 根据ParentID获取行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAreaListByParentID(AreaQuery query)
        {
            List<Area> list = new List<Area>();
            list = await _comService.GetAreaListByParentID(query);
            return Json(new { code = 1, data = list, msg = "Success" });
        }

        /// <summary>
        /// 获取行政区划实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetAreaModel(AreaQuery query)
        {
            if (query.ID == 0)
                query.ID = 2;
            Area model = _comService.GetAreaModel(query);
            if (model != null)
                return Json(new { code = 1, data = model, msg = "Success" });
            else
                return Json(new { code = 0, msg = "" });
        }

        /// <summary>
        /// 组装上级行政区划下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MakeAreaTreeSelect()
        {
            AreaQuery query = new AreaQuery();
            query.ParentID = 1;
            query.AreaLevel = 2;
            TreeSelectModel model = await _comService.MakeAreaTreeSelect(query);
            if (model != null && model.TreeSelectList.Count > 0)
                return Json(model.TreeSelectList);
            else
                return Json("");
        }
    }
}
