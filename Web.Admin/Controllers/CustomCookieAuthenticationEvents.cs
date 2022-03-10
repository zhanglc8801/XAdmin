using XAdmin.Common;
using XAdmin.IService;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Admin.Controllers
{
    public class CustomCookieAuthenticationEvents: CookieAuthenticationEvents
    {
        private readonly IUserInfoService _userInfoService;

        public CustomCookieAuthenticationEvents(IUserInfoService userInfoService)
        {
            // 从DI 里面获取用户相关的.
            _userInfoService = userInfoService;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
            long UserId = string.IsNullOrEmpty(userPrincipal.FindFirst("UserId").Value) ? 0 : long.Parse(userPrincipal.FindFirst("UserId").Value);

            // 查找上面的LastChanged相关的claim.
            var lastChanged = (from c in userPrincipal.Claims
                               where c.Type == "LastChanged"
                               select c.Value).FirstOrDefault();

            UserInfoQuery query = new UserInfoQuery();
            query.SiteID = SiteConfigManager.SiteID;
            query.ID = UserId;
            query.lastChanged = lastChanged;
            if (string.IsNullOrEmpty(lastChanged) ||
                !_userInfoService.ValidateLastChanged(query))//调用的ValidateLastChanged来判断这个lastChanged 相关的额 cookie是否是一个有效的cookie
            {
                context.RejectPrincipal();//拒绝这个 cookie
                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);// 自动登出
            }
        }
        ////其他的方法，都可以设置
        //public override Task SignedIn(CookieSignedInContext context)
        //{
        //    return base.SignedIn(context);
        //}
    }
}
