using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using XAdmin.Model.Enum;

namespace Web.Admin.Controllers
{
    public class SystemManagementController : BaseController
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISystemManagementService _sysService;
        public SystemManagementController(IWebHostEnvironment env,ISystemManagementService sysService)
        {
            this._env = env;
            this._sysService = sysService;
        }

        #region 菜单管理
        public IActionResult Menu()
        {
            return View();
        }

        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetMenuList()
        {
            QueryBase query = new QueryBase();
            query.SiteID = CurUser.UserModel.SiteID;
            List<Menu> list = _sysService.GetMenuList(query);
            if (list.Count > 0)
                return Json(new { code = 0, data = list, msg = "", count = list.Count });
            else
                return Json(new { code = 1, msg = "没有获取到数据!" });
        }

        /// <summary>
        /// 获取含有角色的所有菜单列表
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public IActionResult GetSysMenuListWithRole(int RoleID)
        {
            QueryBase query = new QueryBase();
            query.Int32Key = RoleID;
            query.SiteID = CurUser.UserModel.SiteID;
            IList<RoleMenuModel> list = _sysService.GetSysMenuListWithRole(query);
            if (list.Count > 0)
                return Json(new { code = 1, data = list, msg = "", count = list.Count });
            else
                return Json(new { code = 0, msg = "没有获取到数据!" });
        }

        /// <summary>
        /// 更新菜单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateMenuAjax(MenuQuery model)
        {
            model.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = await _sysService.UpdateMenuAsync(model);
            if (result.code == 1)
                return Json(new { code = 0, result = Result.success, msg = "操作成功。" });
            else
                return Json(new { code = 1, result = Result.failure, msg = "操作失败！" });
        }

        /// <summary>
        /// 修改菜单信息 视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult MenuEdit(int ID = 0, int PID = 0)
        {
            Menu model = new Menu();
            if (ID != 0)
            {
                MenuQuery query = new MenuQuery();
                query.SiteID = CurUser.UserModel.SiteID;
                query.MenuID = ID;
                model = _sysService.GetMenuModel(query);
                //model.Icon = WebUtility.UrlDecode(model.Icon);
                return View(model);
            }
            else
            {
                model.MenuID = 0;
                model.MenuName = "";
                model.MenuUrl = "";
                model.PMenuID = PID;
                model.Icon = "";
                model.Status = true;
                return View(model);
            }
        }

        /// <summary>
        /// 组装菜单下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MakeTreeSelect()
        {
            QueryBase query = new QueryBase();
            query.SiteID = CurUser.UserModel.SiteID;
            TreeSelectModel model = await _sysService.MakeTreeSelect(query);
            if (model != null && model.TreeSelectList.Count > 0)
                return Json(model.TreeSelectList);
            else
                return Json("");
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isroot">是否是一级菜单</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveMenuAjax(Menu model, string isroot)
        {
            model.SiteID = CurUser.UserModel.SiteID;
            if (isroot == "on")
                model.PMenuID = 0;
            ExecResult result = _sysService.SaveMenu(model);
            return Json(new { code = result.code, msg = result.msg });
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteMenu(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _sysService.DeleteMenu(query);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        public IActionResult Area()
        {
            return View();
        }

        /// <summary>
        /// 获取所有行政区划列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetAreaList()
        {
            QueryBase query = new QueryBase();
            AreaModel model = await _sysService.GetAreaList(query);
            if (model!=null && model.AreaList.Count > 0)
                return Json(new { code = 0, data = model.AreaList, msg = "", count = model.AreaList.Count });
            else
                return Json(new { code = 1, msg = "没有获取到数据!" });
        }

        /// <summary>
        /// 更新行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateAreaAjax(QueryBase model)
        {
            ExecResult result = new ExecResult();
            result = await _sysService.UpdateAreaAsync(model);
            if (result.code == 1)
                return Json(new { code = 0, result = Result.success, msg = "操作成功。" });
            else
                return Json(new { code = 1, result = Result.failure, msg = "操作失败！" });
        }

        /// <summary>
        /// 删除行政区划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAreaAjax(Area model)
        {
            ExecResult result = new ExecResult();
            result = await _sysService.DeleteAreaAsync(model);
            if (result.code == 1)
                return Json(new { code = 0, result = Result.success, msg = "操作成功。" });
            else
                return Json(new { code = 1, result = Result.failure, msg = "操作失败！" });
        }

        /// <summary>
        /// 设置行政区划禁用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetAreaStatusAjax(Area model)
        {
            ExecResult result = new ExecResult();
            result = await _sysService.SetAreaStatusAsync(model);
            if (result.code == 1)
                return Json(new { code = 0, result = Result.success, msg = "操作成功。" });
            else
                return Json(new { code = 1, result = Result.failure, msg = "操作失败！" });
        }

        public IActionResult AreaAdd()
        {
            return View();
        }

        /// <summary>
        /// 保存行政区划修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveAreaAjax(Area model)
        {
            ExecResult result = _sysService.SaveArea(model);
            if (result.code == 1)
                return Json(new { code = 0, result = Result.success, msg = "保存成功。" });
            else
                return Json(new { code = 1, result = Result.failure, msg = "保存失败！" });
        }

    }
}
