using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using XAdmin.Model.Enum;

namespace Web.Admin.Controllers
{
    public class RightsManagementController : BaseController
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IRoleInfoService _roleInfoService;
        private readonly IGroupInfoService _groupInfoService;
        public RightsManagementController(IUserInfoService userInfoService, IRoleInfoService roleInfoService, IGroupInfoService groupInfoService)
        {
            this._userInfoService = userInfoService;
            this._roleInfoService = roleInfoService;
            this._groupInfoService = groupInfoService;
        }

        #region 用户管理
        /// <summary>
        /// 用户列表视图页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserList()
        {
            QueryBase qb = new QueryBase();
            List<RoleInfo> RoleList = new List<RoleInfo>();
            qb.SiteID = CurUser.UserModel.SiteID;
            RoleList = await _roleInfoService.GetRoleList(qb);
            ViewBag.RoleList = RoleList;
            return View();
        }

        /// <summary>
        /// 用户新增/编辑视图页
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ActionResult> UserEdit(UserInfoQuery query)
        {
            QueryBase qb = new QueryBase();
            List<RoleInfo> RoleList = new List<RoleInfo>();
            qb.SiteID = CurUser.UserModel.SiteID;
            RoleList = await _roleInfoService.GetRoleList(qb);
            ViewBag.RoleList = RoleList;
            if (query.ID > 0)
            {
                UserModel model = await _userInfoService.GetUserModel(query);
                ViewBag.RoleUserList = model.RoleUserList;//当前用户所属角色列表
                return View(model);
            }
            else
            {
                return View(new UserModel());
            }
        }

        public async Task<ActionResult> UserShow(UserInfoQuery query)
        {
            QueryBase qb = new QueryBase();
            List<RoleInfo> RoleList = new List<RoleInfo>();
            qb.SiteID = CurUser.UserModel.SiteID;
            RoleList = await _roleInfoService.GetRoleList(qb);
            ViewBag.RoleList = RoleList;
            UserModel model = await _userInfoService.GetUserModel(query);
            ViewBag.RoleUserList = model.RoleUserList;//当前用户所属角色列表
            return View(model);
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetUserPageList(int page, int limit, String JsonStr)
        {
            UserInfoQuery query = new UserInfoQuery();
            if (JsonStr != null)
            {
                query = JsonConvert.DeserializeObject<UserInfoQuery>(JsonStr);
            }
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            UserInfoListModel model = _userInfoService.GetUserPageList(query);
            if (model != null && model.UserInfoList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.UserInfoList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "Get Data Failed！" });
        }

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ChangeUserStatus(UserInfoQuery query)
        {
            var data = await _userInfoService.ChangeUserStatus(query);
            return Json(new { code = data.code, msg = "Success！" });
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="model2"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateUserInfo(string JsonStr, string RoleIds)
        {
            UserInfo model = JsonConvert.DeserializeObject<UserInfo>(JsonStr);
            UserDetail model2 = JsonConvert.DeserializeObject<UserDetail>(JsonStr);

            ExecResult result = new ExecResult();
            result = await _userInfoService.UpdateUserInfoAsync(model);
            //更新用户扩展表信息
            if (result.code == 1)
            {
                await _userInfoService.UpdateUserDetailAsync(model2);
            }
            //更改用户密码
            if (model.Pwd != "")
            {
                ChangePwd cp = new ChangePwd();
                cp.UserID = model.ID;
                cp.NewPwd = model.Pwd;
                await _userInfoService.SetNewPassword(cp);
            }
            //设置用户角色
            if (result.code == 1)
            {
                SetUserRoleModel rm = new SetUserRoleModel();
                rm.SiteID = CurUser.UserModel.SiteID;
                rm.UserID = model.ID;
                rm.RoleIdStr = RoleIds;
                await _roleInfoService.SetUserRole(rm);
            }
            return Json(new { code = result.code, msg = result.msg });
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteUserInfo(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            if (query.KeyStr.Contains(CurUser.UserModel.ID + ","))
            {
                return Json(new { code = 0, msg = "不允许删除当前登录用户！" });
            }
            else
            {
                ExecResult result = _userInfoService.DeleteUserInfo(query);
                return Json(new { code = result.code, msg = result.msg });
            }  
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 角色列表视图页
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleList()
        {
            return View();
        }

        /// <summary>
        /// 角色新增/编辑视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult RoleEdit(int ID = 0)
        {
            RoleInfo model = new RoleInfo();
            if (ID != 0)
            {
                QueryBase query = new QueryBase();
                query.Int32Key = ID;
                model = _roleInfoService.GetRoleModel(query);
                return View(model);
            }
            else
            {
                model.ID = 0;
                model.RoleName = "";
                model.Note = "";
                return View(model);
            }
        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetRolePageList(int page, int limit, QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            RoleInfoListModel model = _roleInfoService.GetRolePageList(query);
            if (model != null && model.RoleInfoList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.RoleInfoList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "Get Data Failed！" });

        }

        ///// <summary>
        ///// 保存角色信息
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult SaveRoleInfo(RoleInfo model)
        //{
        //    ExecResult result = new ExecResult();
        //    model.SiteID = CurUser.UserModel.SiteID;
        //    model.AddDate = DateTime.Now;
        //    result = _roleInfoService.SaveRoleInfo(model);
        //    if (result.code == 1)
        //    {
        //        if (model.ID > 0)
        //            return Json(new { code = 0, result = result.success, msg = "角色信息更新成功。" });
        //        else
        //            return Json(new { code = 1, result = result.failure, msg = "添加角色信息成功。" });
        //    }
        //    else
        //        return Json(new { code = 1, msg = result.msg });
        //}

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveRoleMenuAuth(RoleAuthListModel model)
        {
            ExecResult result = new ExecResult();

            //保存角色信息
            RoleInfo role = new RoleInfo();
            role.SiteID = CurUser.UserModel.SiteID;
            role.ID = model.RoleID;
            role.RoleName = model.RoleName;
            role.Note = model.Note;
            result = _roleInfoService.SaveRoleInfo(role);
            if (result.code == 1)
            {
                if (model.RoleID == 0)
                    model.RoleID = int.Parse(result.ReturnInt.ToString());
                //保存角色菜单权限
                //35|36|37|38|40|
                //36|add;36|edit;36|del;37|add;37|edit;37|del;38|add;38|edit;38|del;40|add;40|edit;40|del;
                if (model.MenuAuthStr == null)
                    model.MenuAuthStr = "";
                if (model.MenuIDStr == null)
                    model.MenuIDStr = "";

                string[] MenuIDs = model.MenuIDStr.Split('|', StringSplitOptions.RemoveEmptyEntries);
                string[] MenuAuths = model.MenuAuthStr.Split(';', StringSplitOptions.RemoveEmptyEntries);
                string[] auths = { };
                string authStr = "";
                List<RoleMenu> list = new List<RoleMenu>();
                foreach (string menuId in MenuIDs)
                {
                    authStr = "";
                    foreach (string MenuAuth in MenuAuths)
                    {
                        auths = MenuAuth.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        if (menuId == auths[0])
                        {
                            authStr += auths[1] + "|";
                        }
                    }

                    list.Add(new RoleMenu
                    {
                        RMID = System.Guid.NewGuid().ToString(),
                        SiteID=CurUser.UserModel.SiteID,
                        MenuID = int.Parse(menuId),
                        RoleID = model.RoleID,
                        AuthStr = authStr,
                        AddDate = DateTime.Now
                    });
                }
                model.RoleAuthList = list;
                result = _roleInfoService.SaveRoleMenuAuth(model);
                if (result.code == 1)
                    return Json(new { code = 1, msg = result.msg });
                else
                    return Json(new { code = 0, msg = result.msg });
            }
            else
            {
                return Json(new { code = 0, msg = result.msg });
            }
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteRoleInfo(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _roleInfoService.DeleteRoleInfo(query);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        #region 用户组管理
        /// <summary>
        /// 用户组列表视图页
        /// </summary>
        /// <returns></returns>
        public ActionResult UserGroupList()
        {
            return View();
        }

        /// <summary>
        /// 用户组新增/编辑视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult UserGroupEdit(int ID = 0)
        {
            GroupInfo model = new GroupInfo();
            if (ID != 0)
            {
                QueryBase query = new QueryBase();
                query.Int32Key = ID;
                model = _groupInfoService.GetUserGroupModel(query);
                return View(model);
            }
            else
            {
                model.ID = 0;
                model.GroupName = "";
                model.Note = "";
                return View(model);
            }
        }

        /// <summary>
        /// 获取用户组分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetGroupInfoPageList(int page, int limit, QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            GroupInfoListModel model = _groupInfoService.GetGroupInfoPageList(query);
            if (model != null && model.GroupInfoList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.GroupInfoList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "Get Data Failed！" });

        }

        /// <summary>
        /// 更改用户组状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ChangeGroupStatus(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            var data = await _groupInfoService.ChangeGroupStatus(query);
            return Json(new { code = data.code, msg = "Success！" });
        }

        /// <summary>
        /// 保存用户组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveGroupInfo(GroupInfo model)
        {
            ExecResult result = new ExecResult();
            model.SiteID = CurUser.UserModel.SiteID;
            model.Status = true;
            model.AddDate = DateTime.Now;
            result = _groupInfoService.SaveGroupInfo(model);
            return Json(new { code = result.code, msg = result.msg });
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteGroupInfo(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _groupInfoService.DeleteGroupInfo(query);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        #region 用户组成员管理
        /// <summary>
        /// 用户组成员列表 视图页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserGroupMembers()
        {
            QueryBase query = new QueryBase();
            query.SiteID = CurUser.UserModel.SiteID;
            IList<GroupInfo> _GroupInfoList = new List<GroupInfo>();
            _GroupInfoList = await _groupInfoService.GetGroupInfoList(query);
            ViewBag.GroupInfoList = _GroupInfoList;
            return View();
        }

        /// <summary>
        /// 获取用户组成员分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetGroupUserList(int page, int limit, String JsonStr)
        {
            GroupUserQuery query = new GroupUserQuery();
            if (JsonStr != null)
            {
                query = JsonConvert.DeserializeObject<GroupUserQuery>(JsonStr);
            }
            query.SiteID = CurUser.UserModel.SiteID;
            query.CurrentPage = page;
            query.PageSize = limit;
            GroupUserEx model = _groupInfoService.GetGroupUserList(query);

            if (model != null && model.list.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.list, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "没有获取到符合条件的数据！" });

        }

        /// <summary>
        /// 从用户组中移除成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveGroupUser(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _groupInfoService.RemoveGroupUser(query);
            return Json(new { code = result.code, msg = result.msg });
        }

        /// <summary>
        /// 向用户组添加成员 视图页
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> AddUserGroupMember(int GroupID)
        {
            QueryBase qb = new QueryBase();
            List<RoleInfo> RoleList = new List<RoleInfo>();
            qb.SiteID = CurUser.UserModel.SiteID;
            RoleList = await _roleInfoService.GetRoleList(qb);
            ViewBag.RoleList = RoleList;
            ViewBag.GroupID = GroupID;
            return View();
        }

        /// <summary>
        /// 获取未加入用户组的成员列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="JsonStr"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetNotGroupUserList(int page, int limit, int GroupID, string JsonStr)
        {
            GroupUserQuery query = new GroupUserQuery();
            if (JsonStr != null)
            {
                query = JsonConvert.DeserializeObject<GroupUserQuery>(JsonStr);
            }
            query.SiteID = CurUser.UserModel.SiteID;
            query.GroupID = GroupID;
            query.CurrentPage = page;
            query.PageSize = limit;
            GroupUserEx model = _groupInfoService.GetNotGroupUserList(query);
            if (model != null && model.UserList.Count > 0)
                return Json(new { code = 0, count = model.TotalRecords, data = model.UserList, msg = "Get Data Success！" });
            else
                return Json(new { code = 1, count = 0, msg = "Get Data Failed！" });

        }

        /// <summary>
        /// 向用户组添加成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddGroupUserAjax(AddGroupUser query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            ExecResult result = _groupInfoService.AddGroupUser(query);
            return Json(new { code = result.code, msg = result.msg });
        }
        #endregion

        

    }
}