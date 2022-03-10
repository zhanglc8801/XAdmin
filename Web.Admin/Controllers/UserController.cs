using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using XAdmin.Common;
using XAdmin.Common.Security;
using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserInfoService _userInfoService;

        public UserController(IUserInfoService userInfoService)
        {
            this._userInfoService = userInfoService;
        }

        /// <summary>
        /// 验证码Key
        /// </summary>
        private const string VERFIYCODEKEY = "XADMIN_AUTH_VERFIFYCODE";
        private const string PreLoginUserName = "XADMIN_PRELOGINUSERNAME";
        //private const string PreLoginUserID = "FASTPLAT_PRELOGINUSERID";

        public IActionResult Login()
        {
            # region 得到上一次的登录名
            string userName = CookieHelper.GetCookie(PreLoginUserName);
            ViewBag.UserName = HttpUtility.UrlDecode(userName);
            # endregion
            return View();
        }

        [HttpPost]
        public JsonResult LoginAjax(string LoginName, string Pwd, string VerifyCode, int IsAutoLogin, string SiteID)
        {
            #region 得到验证码
            string code = CookieHelper.GetCookie(VERFIYCODEKEY);
            #endregion

            if (XAdmin.Common.Security.DES.Encrypt(VerifyCode.ToLower()) == code)
            {
                UserInfoQuery query = new UserInfoQuery();
                query.UserName = LoginName;
                query.Pwd = Pwd;
                query.SiteID = SiteID;
                LoginUserInfo model = _userInfoService.UserLogin(query);
                if (model.UserModel != null)
                {
                    if (model.UserModel.Status == 0)
                        return base.Json(new { flag = -2 });//帐号未激活
                    else
                    {
                        LoginRoleModel LoginRole = new LoginRoleModel();
                        if (model.UserModel.LoginRole == 0)
                        {
                            LoginRole.RoleID = model.RoleList[0].RoleID;
                            LoginRole.RoleName = model.RoleList[0].RoleName;
                        }
                        else
                        {
                            LoginRole.RoleID = model.UserModel.LoginRole;
                            LoginRole.RoleName = model.RoleList.Where(x => x.RoleID == model.UserModel.LoginRole).Select(x => x.RoleName).FirstOrDefault();
                        }
                        model.UserModel.IsAutoLogin = IsAutoLogin;
                        model.CurRole = LoginRole;

                        #region 修改登录信息
                        UserInfo loginModel = new UserInfo();
                        loginModel.ID = model.UserModel.ID;
                        loginModel.LoginCount = model.UserModel.LoginCount + 1;
                        loginModel.LoginDate = DateTime.Now;
                        loginModel.LoginIP = XAdmin.Common.Utils.PublicMethod.GetRealIP();
                        loginModel.LoginRole = model.RoleList[0].RoleID;
                        _userInfoService.UpdateLoginInfo(loginModel);

                        #endregion
                        // 记录登录名Cookie
                        CookieHelper.SetCookie(PreLoginUserName, HttpUtility.UrlEncode(model.UserModel.UserName), 360);
                        // 保存登录ticket
                        SaveLoginTicket(model.UserModel.ID.ToString(), JsonConvert.SerializeObject(model), IsAutoLogin);
                        return base.Json(new { flag = 1 });
                    }
                }
                else
                {
                    return base.Json(new { flag = 0 });//登录失败
                }

            }
            else
                return base.Json(new { flag = -1 });//验证码错误
        }

        /// <summary>
        /// 更改当前角色登录
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChanageCurRole(long UserID, int RoleID, string RoleName, int IsAutoLogin, string SiteID)
        {
            ExecResult result = new ExecResult();
            try
            {
                QueryBase query = new QueryBase();
                query.SiteID = SiteID;
                query.Int64Key = UserID;
                List<int> RoleIDList = _userInfoService.GetUserRoleIDList(query);
                if (RoleIDList.Any(x => x == RoleID))
                {
                    LoginUserInfo model = JsonConvert.DeserializeObject<LoginUserInfo>(User.FindFirst("UserData").Value);
                    model.CurRole = new LoginRoleModel { RoleID = RoleID, RoleName = RoleName };
                    model.UserModel.LoginRole = RoleID;
                    model.UserModel.IsAutoLogin = IsAutoLogin;
                    #region 修改登录信息
                    UserInfo loginModel = new UserInfo();
                    loginModel.ID = UserID;
                    loginModel.LoginCount = model.UserModel.LoginCount + 1;
                    loginModel.LoginDate = DateTime.Now;
                    loginModel.LoginIP = XAdmin.Common.Utils.PublicMethod.GetRealIP();
                    loginModel.LoginRole = RoleID;
                    _userInfoService.UpdateLoginInfo(loginModel);
                    #endregion
                    //保存登录ticket
                    SaveLoginTicket(model.UserModel.ID.ToString(), JsonConvert.SerializeObject(model), IsAutoLogin);
                    result.code = 1;
                }
                else
                {
                    result.code = 0;
                    result.msg = "无法使用指定的角色进行登录！因为当前登录用户已在该角色中被移除。";
                }
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            return Content(JsonConvert.SerializeObject(result));
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <returns></returns>
        public ActionResult ChangePwd(string SiteID, long UserID)
        {
            ViewBag.SiteID = SiteID;
            ViewBag.UserID = UserID;
            return View();
        }

        [HttpPost]
        public JsonResult ChangePwdAjax(ChangePwd model)
        {
            ExecResult result = _userInfoService.ChangePassword(model);
            return Json(result);
        }

        public void Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContextAccessor _context = new HttpContextAccessor();
            _context.HttpContext.Response.Redirect("/user/login");
        }

        private void SaveLoginTicket(string userID, string userData, int autologin)
        {
            int ExpireDays = Convert.ToInt32(ConfigurationManager.GetJson("Strings", "CookieExpires"));//过期天数
            if (autologin == 0)
                ExpireDays = 1;
            var claims = new List<Claim>
            {
                new Claim("UserID", userID),
                new Claim("UserData", userData),
                new Claim("autologin",autologin.ToString()),
                new Claim("ExpireDays",ExpireDays.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            if (autologin == 1)
            {
                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                //Cookie持久化设置
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(ExpireDays).AddHours(8)
                });
            }
            else
            {
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));
            } 
        }

        

    }
}