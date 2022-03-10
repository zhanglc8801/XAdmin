using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XAdmin.IRepository
{
    public interface IRoleInfoRepository
    {
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        RoleInfoListModel GetRolePageList(QueryBase query);

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<RoleInfo>> GetRoleList(QueryBase query);

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        RoleInfo GetRoleModel(QueryBase query);

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveRoleInfo(RoleInfo model);

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveRoleMenuAuth(RoleAuthListModel model);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        ExecResult DeleteRoleInfo(QueryBase query);

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> SetUserRole(SetUserRoleModel model);

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<RoleMenuModel> GetRoleMenuList(RoleMenuQuery query);

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SetRoleMenu(SetRoleMenu model);
    }
}
