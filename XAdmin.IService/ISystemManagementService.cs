using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.IService
{
    public interface ISystemManagementService
    {
        #region 菜单管理
        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<Menu> GetMenuList(QueryBase query);

        /// <summary>
        /// 获取含有角色的所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<RoleMenuModel> GetSysMenuListWithRole(QueryBase query);

        /// <summary>
        /// 获取菜单实体
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        Menu GetMenuModel(MenuQuery query);

        /// <summary>
        /// 异步更新菜单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> UpdateMenuAsync(MenuQuery model);

        /// <summary>
        /// 组装树形菜单下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TreeSelectModel> MakeTreeSelect(QueryBase query);

        /// <summary>
        /// 组装菜单图标下拉框IconSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TreeSelectModel> MakeIconSelect(QueryBase query);

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveMenu(Menu model);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteMenu(QueryBase query);
        #endregion

        #region 行政区划管理
        /// <summary>
        /// 异步获取所有行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<AreaModel> GetAreaList(QueryBase query);

        /// <summary>
        /// 异步获取行政区划分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<AreaModel> GetAreaPageList(QueryBase query);

        /// <summary>
        /// 异步更新行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> UpdateAreaAsync(QueryBase model);

        /// <summary>
        /// 删除行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> DeleteAreaAsync(Area model);

        /// <summary>
        /// 设置行政区划禁用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> SetAreaStatusAsync(Area model);

        /// <summary>
        /// 保存行政区划修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveArea(Area model);
        #endregion
    }
}
