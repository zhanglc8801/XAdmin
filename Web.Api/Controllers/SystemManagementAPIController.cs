using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    /// <summary>
    /// 系统管理类操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SystemManagementAPIController : ControllerBase
    {
        private ISystemManagementRepository _sys;
        public SystemManagementAPIController(ISystemManagementRepository sys)
        {
            this._sys = sys;
        }

        #region 菜单管理
        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<Menu> GetMenuList(QueryBase query)
        {
            return _sys.GetMenuList(query);
        }

        /// <summary>
        /// 获取含有角色的所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IList<RoleMenuModel> GetSysMenuListWithRole(QueryBase query)
        {
            return _sys.GetSysMenuListWithRole(query);
        }

        /// <summary>
        /// 获取菜单实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Menu GetMenuModel(MenuQuery query)
        {
            return _sys.GetMenuModel(query);
        }

        /// <summary>
        /// 异步更新菜单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> UpdateMenu(MenuQuery model)
        {
            return await _sys.UpdateMenuAsync(model);
        }

        /// <summary>
        /// 组装树形菜单下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TreeSelectModel> MakeTreeSelect(QueryBase query)
        {
            return await _sys.MakeTreeSelect(query);
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveMenu(Menu model)
        {
            return _sys.SaveMenu(model);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteMenu(QueryBase query)
        {
            return _sys.DeleteMenu(query);
        }
        #endregion

        #region 行政区划管理
        /// <summary>
        /// 异步获取所有行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AreaModel> GetAreaList(QueryBase query)
        {
            return await _sys.GetAreaList(query);
        }

        /// <summary>
        /// 异步获取行政区划分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AreaModel> GetAreaPageList(QueryBase query)
        {
            return await _sys.GetAreaPageList(query);
        }

        /// <summary>
        /// 异步更新行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> UpdateArea(QueryBase model)
        {
            return await _sys.UpdateAreaAsync(model);
        }

        /// <summary>
        /// 删除行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> DeleteArea(Area model)
        {
            return await _sys.DeleteAreaAsync(model);
        }

        /// <summary>
        /// 设置行政区划禁用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> SetAreaStatus(Area model)
        {
            return await _sys.SetAreaStatusAsync(model);
        }

        /// <summary>
        /// 保存行政区划修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveArea(Area model)
        {
            return _sys.SaveArea(model);
        }
        #endregion

    }
}
