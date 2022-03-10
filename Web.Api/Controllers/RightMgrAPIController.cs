using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.Repository;
using XAdmin.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    /// <summary>
    /// 权限操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RightMgrAPIController : ControllerBase
    {
        private IRoleInfoRepository _role;
        private IGroupInfoRepository _group;
        public RightMgrAPIController(IRoleInfoRepository role, IGroupInfoRepository group)
        {
            this._role = role;
            this._group = group;
        }

        #region 用户组管理API
        /// <summary>
        /// 获取用户组分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public GroupInfoListModel GetGroupInfoPageList(QueryBase query)
        {
            return _group.GetGroupInfoPageList(query);
        }

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IList<GroupInfo>> GetGroupInfoList(QueryBase query)
        {
            return await _group.GetGroupInfoList(query);
        }

        /// <summary>
        /// 获取用户组信息
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        [HttpPost]
        public GroupInfo GetUserGroupModel(QueryBase query)
        {
            return _group.GetUserGroupModel(query);
        }

        /// <summary>
        /// 更改用户组状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> ChangeGroupStatus(QueryBase query)
        {
            return await _group.ChangeGroupStatus(query);
        }

        /// <summary>
        /// 保存用户组信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveGroupInfo(GroupInfo model)
        {
            return _group.SaveGroupInfo(model);
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteGroupInfo(QueryBase query)
        {
            return _group.DeleteGroupInfo(query);
        }
        #endregion

        #region 用户组成员管理API
        /// <summary>
        /// 获取用户组中的用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public GroupUserEx GetGroupUserList(GroupUserQuery model)
        {
            return _group.GetGroupUserList(model);
        }

        /// <summary>
        /// 从用户组中移除成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult RemoveGroupUser(QueryBase query)
        {
            return _group.RemoveGroupUser(query);
        }

        /// <summary>
        /// 获取未加入用户组的成员列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public GroupUserEx GetNotGroupUserList(GroupUserQuery query)
        {
            return _group.GetNotGroupUserList(query);
        }

        /// <summary>
        /// 加入用户组成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult AddGroupUser(AddGroupUser query)
        {
            return _group.AddGroupUser(query);
        }
        #endregion

        #region 角色管理API
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public RoleInfoListModel GetRolePageList(QueryBase query)
        {
            return _role.GetRolePageList(query);
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<RoleInfo>> GetRoleList(QueryBase query)
        {
            return await _role.GetRoleList(query);
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public RoleInfo GetRoleModel(QueryBase query)
        {
            return _role.GetRoleModel(query);
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveRoleInfo(RoleInfo model)
        {
            return _role.SaveRoleInfo(model);
        }

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveRoleMenuAuth(RoleAuthListModel model)
        {
            return _role.SaveRoleMenuAuth(model);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteRoleInfo(QueryBase query)
        {
            return _role.DeleteRoleInfo(query);
        }
        #endregion

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> SetUserRole(SetUserRoleModel model)
        {
            return await _role.SetUserRole(model);
        }

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IList<RoleMenuModel> GetRoleMenuList(RoleMenuQuery query)
        {
            return _role.GetRoleMenuList(query);
        }

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SetRoleMenu(SetRoleMenu query)
        {
            return _role.SetRoleMenu(query);
        }

    }
}