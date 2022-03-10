using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;

namespace XAdmin.IRepository
{
    public interface IUserInfoRepository
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="queryAuthor"></param>
        /// <returns></returns>
        LoginUserInfo UserLogin(UserInfoQuery queryAuthor);

        /// <summary>
        /// 修改用户登录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult UpdateLoginInfo(UserInfo model);

        /// <summary>
        /// 取用户角色ID列表 用于切换角色时获取后验证
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<int> GetUserRoleIDList(QueryBase query);

        /// <summary>
        /// 更改密码(验证旧密码)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult ChangePassword(ChangePwd model);

        /// <summary>
        /// 设置新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> SetNewPassword(ChangePwd model);

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        UserInfoListModel GetUserPageList(UserInfoQuery query);

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ExecResult> ChangeUserStatus(UserInfoQuery query);

        /// <summary>
        /// 获取用户信息实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<UserModel> GetUserModel(UserInfoQuery query);

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> UpdateUserInfoAsync(UserInfo model);

        /// <summary>
        /// 修改用户扩展信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> UpdateUserDetailAsync(UserDetail model);

        /// <summary>
        /// 更改用户头像
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult ChangeUserPhoto(UserInfo model);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteUserInfo(QueryBase query);

        /// <summary>
        /// 用于验证用户信息是否进行了更改
        /// </summary>
        /// <param name="lastChanged"></param>
        /// <returns></returns>
        bool ValidateLastChanged(UserInfoQuery query);
    }
}
