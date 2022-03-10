using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using XAdmin.IRepository;

namespace Web.Api.Controllers
{
    /// <summary>
    /// 用户操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserInfoAPIController : ControllerBase
    {
        private IUserInfoRepository _repository;
        public UserInfoAPIController(IUserInfoRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public LoginUserInfo UserLogin(UserInfoQuery query)
        {
            LoginUserInfo user = _repository.UserLogin(query);
            return user;
        }

        ///// <summary>
        ///// token登录
        ///// </summary>
        ///// <param name="securityKey"></param>
        ///// <param name="userName"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult JwtLogin([FromServices] SymmetricSecurityKey securityKey, string userName)
        //{
        //    List<Claim> claims = new List<Claim>();
        //    claims.Add(new Claim("Name", userName));
        //    var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var token = new JwtSecurityToken(
        //        issuer: "localhost",
        //        audience: "localhost",
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(30),
        //        signingCredentials: creds
        //        );
        //    var t = new JwtSecurityTokenHandler().WriteToken(token);
        //    var result = User.Identity.IsAuthenticated;
        //    return Content(t + "----" + result.ToString());
        //}

        /// <summary>
        /// 更新用户登录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult UpdateLoginInfo(UserInfo model)
        {
            ExecResult result = _repository.UpdateLoginInfo(model);
            return result;
        }

        /// <summary>
        /// 取用户角色ID列表 用于切换角色时获取后验证
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<int> GetUserRoleIDList(QueryBase query)
        {
            return _repository.GetUserRoleIDList(query);
        }

        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult ChangePassword(ChangePwd model)
        {
            return _repository.ChangePassword(model);
        }

        /// <summary>
        /// 设置新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> SetNewPassword(ChangePwd model)
        {
            return await _repository.SetNewPassword(model);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public UserInfoListModel GetUserPageList(UserInfoQuery query)
        {
            return _repository.GetUserPageList(query);
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> ChangeUserStatus(UserInfoQuery query)
        {
            return await _repository.ChangeUserStatus(query);
        }

        /// <summary>
        /// 获取用户信息实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<UserModel> GetUserModel(UserInfoQuery query)
        {
            return await _repository.GetUserModel(query);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> UpdateUserInfo(UserInfo model)
        {
            return await _repository.UpdateUserInfoAsync(model);
        }

        /// <summary>
        /// 修改用户扩展信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> UpdateUserDetail(UserDetail model)
        {
            return await _repository.UpdateUserDetailAsync(model);
        }

        /// <summary>
        /// 更改用户头像 ChangeUserPhoto
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult ChangeUserPhoto(UserInfo model)
        {
            return _repository.ChangeUserPhoto(model);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteUserInfo(QueryBase query)
        {
            return _repository.DeleteUserInfo(query);
        }

    }
}