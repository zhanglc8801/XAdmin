using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XAdmin.Service
{
    public class UserInfoService : ServiceBase, IUserInfoService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public LoginUserInfo UserLogin(UserInfoQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            LoginUserInfo userInfo = clientHelper.PostAuth<LoginUserInfo, UserInfoQuery>(GetAPIUrl(APIConstants.UserLogin), query);
            return userInfo;
        }

        /// <summary>
        /// 修改用户登录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult UpdateLoginInfo(UserInfo model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, UserInfo>(GetAPIUrl(APIConstants.UpdateLoginInfo), model);
            return result;
        }
        /// <summary>
        /// 取用户角色ID列表 用于切换角色时获取后验证
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<int> GetUserRoleIDList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<int> result = clientHelper.PostAuth<List<int>, QueryBase>(GetAPIUrl(APIConstants.GetUserRoleIDList), query);
            return result;
        }

        /// <summary>
        /// 更改密码(验证旧密码)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult ChangePassword(ChangePwd model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, ChangePwd>(GetAPIUrl(APIConstants.ChangePassword), model);
            return result;
        }

        /// <summary>
        /// 设置新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> SetNewPassword(ChangePwd model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, ChangePwd>(GetAPIUrl(APIConstants.SetNewPassword), model);
            return result;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public UserInfoListModel GetUserPageList(UserInfoQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            UserInfoListModel result = clientHelper.PostAuth<UserInfoListModel, UserInfoQuery>(GetAPIUrl(APIConstants.GetUserPageList), query);
            return result;
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeUserStatus(UserInfoQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, UserInfoQuery>(GetAPIUrl(APIConstants.ChangeUserStatus), query);
            return result;
        }

        /// <summary>
        /// 获取用户信息实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserModel(UserInfoQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            UserModel result = await clientHelper.PostAuthAsync<UserModel, UserInfoQuery>(GetAPIUrl(APIConstants.GetUserInfoAsync), query);
            return result;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateUserInfoAsync(UserInfo model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, UserInfo>(GetAPIUrl(APIConstants.UpdateUserInfoAsync), model);
            return result;
        }

        /// <summary>
        /// 修改用户扩展信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateUserDetailAsync(UserDetail model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, UserDetail>(GetAPIUrl(APIConstants.UpdateUserDetailAsync), model);
            return result;
        }

        /// <summary>
        /// 更改用户头像
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult ChangeUserPhoto(UserInfo model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, UserInfo>(GetAPIUrl(APIConstants.ChangeUserPhoto), model);
            return result;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteUserInfo(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteUserInfo), query);
            return result;
        }

        /// <summary>
        /// 用于验证用户信息是否进行了更改
        /// </summary>
        /// <param name="lastChanged"></param>
        /// <returns></returns>
        public bool ValidateLastChanged(UserInfoQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            bool result = clientHelper.PostAuth<bool, UserInfoQuery>(GetAPIUrl(APIConstants.ValidateLastChanged), query);
            return result;
        }
    }
}
