using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Admin.Controllers
{
    
    public class HomeController : BaseController
    {
        private readonly IRoleInfoService _roleInfoService;
        private readonly IUserInfoService _userInfoService;
        public HomeController(IRoleInfoService roleInfoService, IUserInfoService userInfoService)
        {
            this._roleInfoService = roleInfoService;
            this._userInfoService = userInfoService;
        }

        public IActionResult Index()
        {

            if (CurUser != null)
            {
                //获取菜单
                StringBuilder _StringBuilder = new StringBuilder();
                CreateMenus(0, _StringBuilder);
                string str = _StringBuilder.ToString();
                ViewData["MenuHtml"] = str;
            }
            if (CurUser == null)
            {
                CurUser = new LoginUserInfo();
                CurUser.CurRole = new LoginRoleModel();
                CurUser.UserModel = new UserInfoModel();
                CurUser.RoleList = new List<LoginRoleModel>();
            }
            if (CurUser.RoleList.Count > 1)//从角色列表移除当前登录角色
            {
                for (int i = 0; i < CurUser.RoleList.Count; i++)
                {
                    if (CurUser.RoleList[i].RoleID == CurUser.CurRole.RoleID)
                        CurUser.RoleList.RemoveAt(i);
                }
            }
            return View(CurUser);
        }

        public void CreateMenus(int PMenuID, StringBuilder _StringBuilder)
        {
            RoleMenuQuery query = new RoleMenuQuery();
            query.SiteID = CurUser.UserModel.SiteID;
            query.RoleID = CurUser.CurRole.RoleID;
            IList<RoleMenuModel> list = _roleInfoService.GetRoleMenuList(query);
            var MenuList = new List<RoleMenuModel>();
            if (PMenuID == 0)
                MenuList = list.Where(x => x.PMenuID == 0 && x.CHECKED == "true").ToList();
            else
                MenuList = list.Where(x => x.PMenuID == PMenuID && x.CHECKED == "true").ToList();

            if (MenuList.Count > 0)
            {
                foreach (RoleMenuModel item in MenuList)
                {
                    _StringBuilder.Append(string.Format("<li data-name=\"{0}\" class=\"layui-nav-item\">", item.MenuID));
                    var _Child_List = list.Where(w => w.PMenuID != 0 && w.PMenuID == item.MenuID).ToList();
                    if (_Child_List.Count > 0)
                    {
                        _StringBuilder.Append(string.Format("<a href=\"javascript:;\" lay-tips=\"{0}\" lay-direction=\"2\">", item.MenuName));
                        _StringBuilder.Append("<i class=\"layui-icon\" style=\"color:" + item.IconColor + "\">" + WebUtility.UrlDecode(item.Icon) + "</i>");
                        _StringBuilder.Append(string.Format("<cite>{0}</cite>", item.MenuName));
                        _StringBuilder.Append("</a>");
                        _StringBuilder.Append("<dl class=\"layui-nav-child\">");
                        foreach (RoleMenuModel item2 in _Child_List)
                        {
                            _StringBuilder.Append(string.Format("<dd data-name=\"{0}\" >", item2.MenuID));
                            var _ThirdList = list.Where(w => w.PMenuID != 0 && w.PMenuID == item2.MenuID).ToList();
                            if (_ThirdList.Count > 0)
                            {
                                _StringBuilder.Append(string.Format("<a href=\"javascript:;\">{0}</a>", item2.MenuName));
                                _StringBuilder.Append("<dl class=\"layui-nav-child\">");
                                foreach (RoleMenuModel item3 in _ThirdList)
                                {
                                    _StringBuilder.Append(string.Format("<dd data-name=\"{2}\"><a lay-href=\"{0}\">{1}</a></dd>", item3.MenuUrl, item3.MenuName, item3.MenuID));
                                }
                                _StringBuilder.Append("</dl>");
                            }
                            else
                            {
                                //_StringBuilder.Append(string.Format("<a lay-href=\"{0}\">{1}</a>", item2.MenuUrl, item2.MenuName));
                                _StringBuilder.Append(string.Format("<a href=\"javascript:;\" lay-href=\"{0}\" lay-tips=\"{1}\" lay-direction=\"2\">", item2.MenuUrl, item2.MenuName));
                                _StringBuilder.Append("<i class=\"layui-icon\" style=\"color:" + item2.IconColor + "\">" + WebUtility.UrlDecode(item2.Icon) + "</i>");
                                _StringBuilder.Append(string.Format("<cite>{0}</cite>", item2.MenuName));
                                _StringBuilder.Append("</a>");
                            }
                            _StringBuilder.Append("</dd>");
                        }
                        _StringBuilder.Append("</dl>");
                    }
                    else
                    {
                        _StringBuilder.Append(string.Format("<a href=\"javascript:;\" lay-href=\"{0}\" lay-tips=\"{1}\" lay-direction=\"2\">", item.MenuUrl, item.MenuName));
                        _StringBuilder.Append("<i class=\"layui-icon\" style=\"color:" + item.IconColor + "\">" + WebUtility.UrlDecode(item.Icon) + "</i>");
                        _StringBuilder.Append(string.Format("<cite>{0}</cite>", item.MenuName));
                        _StringBuilder.Append("</a>");
                    }
                    _StringBuilder.Append("</li>");
                }
            }
        }

        /// <summary>
        /// 更改当前角色登录
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChanageCurRole(int RoleID, string RoleName)
        {
            ExecResult result = new ExecResult();
            try
            {
                QueryBase query = new QueryBase();
                query.SiteID = CurUser.UserModel.SiteID;
                query.Int64Key = CurUser.UserModel.ID;
                List<int> RoleIDList = _userInfoService.GetUserRoleIDList(query);
                if (RoleIDList.Any(x => x == RoleID))
                {
                    LoginUserInfo model = CurUser;
                    model.CurRole = new LoginRoleModel { RoleID = RoleID, RoleName = RoleName };
                    CookieHelper.SetCookie("XADMIN_USERDATA", JsonConvert.SerializeObject(model));
                    result.code = 1;
                    #region 修改登录信息
                    UserInfo loginModel = new UserInfo();
                    loginModel.ID = model.UserModel.ID;
                    loginModel.LoginCount = model.UserModel.LoginCount;
                    loginModel.LoginDate = DateTime.Now;
                    loginModel.LoginIP = XAdmin.Common.Utils.PublicMethod.GetRealIP();
                    loginModel.LoginRole = RoleID;
                    _userInfoService.UpdateLoginInfo(loginModel);
                    #endregion
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

        public ActionResult Console()
        {
            return View();
        }

    }
}
