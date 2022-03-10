using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.Common;
using XAdmin.Common.Security;
using XAdmin.Model.CustomEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Web.Admin.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        public string SiteID
        {
            get
            {
                return CurUser.UserModel == null ? "" : CurUser.UserModel.SiteID;
            }
        }

        private LoginUserInfo _CurUser;
        public LoginUserInfo CurUser
        {
            get
            {
                _CurUser= JsonConvert.DeserializeObject<LoginUserInfo>(User.FindFirst("UserData").Value);
                return _CurUser;
            }
            set { value = _CurUser; }
        }

        public string AdminUrl
        {
            get
            {
                string strAppPath = ApplicationPath;
                if (!strAppPath.StartsWith("/"))
                {
                    strAppPath = "/" + strAppPath;
                }
                return Request.Scheme + "://" + Request.Host + strAppPath;
            }
        }

        public string ApplicationPath
        {
            get
            {
                if (Request.Path.Value.EndsWith("/"))
                {
                    return Request.Path.Value;
                }
                else
                {
                    return Request.Path.Value + "/";
                }
            }
        }

    }
}