using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.IRepository;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.SqlSugar;

namespace XAdmin.Repository
{
    public class UserInfoRepository: IUserInfoRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="queryAuthor"></param>
        /// <returns></returns>
        public LoginUserInfo UserLogin(UserInfoQuery query)
        {
            LoginUserInfo model = new LoginUserInfo();
            UserInfoModel user = db.Queryable<UserInfo>().Where(x => x.UserName == query.UserName && x.Pwd == XAdmin.Common.Security.MD5Handle.Encrypt(query.Pwd) && x.SiteID == query.SiteID)
                .Select(x => new UserInfoModel
                {
                    ID = x.ID,
                    SiteID = x.SiteID,
                    Status = x.Status.Value,
                    Gender = x.Gender.Value,
                    UserName = x.UserName,
                    UserType = x.UserType.Value,
                    Mobile = x.Mobile,
                    RealName = x.RealName,
                    Photo = x.Photo,
                    LoginCount = x.LoginCount.Value,
                    LoginRole = x.LoginRole.Value
                }).First();
            model.UserModel = user;
            if (user != null)
                model.RoleList = db.Queryable<RoleUser, RoleInfo>((ru, ri) => new object[] { JoinType.Left, ru.RoleID == ri.ID }).Where((ru, ri) => ru.UserID == user.ID).Select((ru, ri) => new LoginRoleModel { RoleID = ru.RoleID.Value, RoleName = ri.RoleName }).ToList();
            return model;
        }

        /// <summary>
        /// 修改用户登录信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult UpdateLoginInfo(UserInfo model)
        {
            int i = db.Updateable<UserInfo>().SetColumns(x => new UserInfo { LoginIP = model.LoginIP, LoginCount = model.LoginCount, LoginDate = model.LoginDate, LoginRole = model.LoginRole }).Where(x => x.ID == model.ID).ExecuteCommand();
            ExecResult result = new ExecResult();
            if (i > 0)
                result.code = 1;
            else
                result.code = 0;
            return result;
        }

        /// <summary>
        /// 获取用户角色ID列表 用于切换角色时获取后验证
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<int> GetUserRoleIDList(QueryBase query)
        {
            return db.Queryable<RoleUser>().Where(x => x.SiteID == query.SiteID && x.UserID == query.Int64Key).Select(x => x.RoleID.Value).ToList();
        }

        /// <summary>
        /// 更改密码(验证旧密码)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult ChangePassword(ChangePwd model)
        {
            ExecResult result = new ExecResult();
            bool b = db.Queryable<UserInfo>().Where(x => x.ID == model.UserID && x.Pwd == XAdmin.Common.Security.MD5Handle.Encrypt(model.OldPwd)).Any();
            if (b)
            {
                string newPwd = XAdmin.Common.Security.MD5Handle.Encrypt(model.NewPwd);
                int i = db.Updateable<UserInfo>().SetColumns(x => new UserInfo { Pwd = newPwd }).Where(x => x.ID == model.UserID).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "密码修改成功！请使用新密码进行登录。";
                }
                else
                {
                    result.code = 0;
                    result.msg = "密码修改失败，请稍后再试！";
                }
            }
            else
            {
                result.code = 0;
                result.msg = "旧密码输入错误！";
            }
            return result;
        }

        /// <summary>
        /// 设置新密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> SetNewPassword(ChangePwd model)
        {
            ExecResult result = new ExecResult();
            string newPwd = XAdmin.Common.Security.MD5Handle.Encrypt(model.NewPwd);
            int i = await db.Updateable<UserInfo>().SetColumns(x => new UserInfo { Pwd = newPwd }).Where(x => x.ID == model.UserID).ExecuteCommandAsync();
            if (i > 0)
            {
                result.code = 1;
                result.msg = "密码修改成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "密码修改失败！";
            }
            return result;
        }

        #region 获取用户分页列表
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public UserInfoListModel GetUserPageList(UserInfoQuery query)
        {
            UserInfoListModel model = new UserInfoListModel();
            int TotalNumber = 0;
            var where = db.Queryable<UserInfo, RoleUser, RoleInfo>((u, ru, ri) => new object[] { JoinType.Inner, u.ID == ru.UserID, JoinType.Left, ru.RoleID == ri.ID }).Where((u, ru, ri) => u.SiteID == query.SiteID);

            if (!string.IsNullOrWhiteSpace(query.UserName))
                where = where.Where((u, ru, ri) => u.UserName.Contains(query.UserName));

            if (!string.IsNullOrWhiteSpace(query.RealName))
                where = where.Where((u, ru, ri) => u.RealName.Contains(query.RealName));

            if (!string.IsNullOrWhiteSpace(query.EMail))
                where = where.Where((u, ru, ri) => u.EMail.Contains(query.EMail));

            if (!string.IsNullOrWhiteSpace(query.Mobile))
                where = where.Where((u, ru, ri) => u.Mobile.Contains(query.Mobile));

            if (query.RoleId > 0)
                where = where.Where((u, ru, ri) => ru.RoleID == query.RoleId);

            if (query.Status != 2)
                where = where.Where((u, ru, ri) => u.Status == query.Status);
            if (query.Gender != 0)
                where = where.Where((u, ru, ri) => u.Gender == query.Gender);
            var getAll = where.OrderBy(u => u.ID).Select((u, ru, ri) => new UserInfoEx
            {
                ID = u.ID,
                SiteID = u.SiteID,
                UserName = u.UserName,
                RoleName = ri.RoleName,
                Pwd = u.Pwd,
                UserType = u.UserType,
                RealName = u.RealName,
                Photo = u.Photo,
                Gender = u.Gender,
                EMail = u.EMail,
                Mobile = u.Mobile,
                LoginIP = u.LoginIP,
                LoginCount = u.LoginCount,
                LoginDate = u.LoginDate,
                LoginRole = u.LoginRole,
                Status = u.Status,
                AddDate = u.AddDate,
                Remark = u.RealName
            });

            model.UserInfoList = getAll.ToPageList(query.CurrentPage, query.PageSize, ref TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        }
        #endregion

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeUserStatus(UserInfoQuery query)
        {
            int i = await db.Updateable<UserInfo>().SetColumns(x => new UserInfo { Status = query.Status.Value }).Where(x => x.ID == query.ID).ExecuteCommandAsync();
            ExecResult result = new ExecResult();
            if (i > 0)
                result.code = 1;
            else
                result.code = 0;
            return result;
        }

        /// <summary>
        /// 获取用户信息实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<UserModel> GetUserModel(UserInfoQuery query)
        {
            var model = await db.Queryable<UserInfo, UserDetail>((ui, ud) => new object[] { JoinType.Left, ui.ID == ud.UserID }).Where((ui, ud) => ui.ID == query.ID).Select<UserModel>().FirstAsync();
            model.RoleUserList = await db.Queryable<RoleUser>().Where(x => x.UserID == query.ID).ToListAsync();
            return model;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateUserInfoAsync(UserInfo model)
        {
            ExecResult result = new ExecResult();
            try
            {
                int i = 0;
                long UserID = 0;
                if (model.ID == 0)
                {
                    UserID = await db.Insertable<UserInfo>(model).IgnoreColumns(new string[] { "ID", "LoginIP", "LoginCount", "LoginDate", "LoginRole", "Photo" }).ExecuteReturnBigIdentityAsync();
                    if (UserID > 0)
                        i = 1;
                }
                else
                    i = await db.Updateable<UserInfo>(model).IgnoreColumns(new string[] { "ID", "SiteID", "Pwd", "UserType", "LoginIP", "LoginCount", "LoginDate", "LoginRole", "Photo", "AddDate" }).Where(x => x.ID == model.ID).ExecuteCommandAsync();
                if (i > 0)
                {
                    result.code = 1;
                    result.ReturnInt64 = UserID;
                    if (model.ID == 0)
                        result.msg = "新增用户信息成功";
                    else
                        result.msg = "用户信息更新成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "用户基本信息更新失败，请稍后再试。";
                }

            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改用户扩展信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateUserDetailAsync(UserDetail model)
        {
            ExecResult result = new ExecResult();
            try
            {
                int i = 0;
                if (model.PKID == 0)
                    i = await db.Insertable<UserDetail>(model).IgnoreColumns(new string[] { "PKID" }).ExecuteCommandAsync();
                else
                    i = await db.Updateable<UserDetail>(model).IgnoreColumns(new string[] { "PKID", "SiteID", "UserID" }).Where(x => x.SiteID == model.SiteID && x.UserID == model.UserID).ExecuteCommandAsync();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "用户扩展信息更新成功";
                }    
                else
                {
                    result.code = 0;
                    result.msg = "用户扩展信息更新失败，请稍后再试。";
                }

            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 更改用户头像
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult ChangeUserPhoto(UserInfo model)
        {
            int i = db.Updateable<UserInfo>().SetColumns(x => new UserInfo { Photo = model.Photo }).Where(x => x.ID == model.ID).ExecuteCommand();
            ExecResult result = new ExecResult();
            if (i > 0)
                result.code = 1;
            else
                result.code = 0;
            return result;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteUserInfo(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<long> list = new List<long>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(int.Parse(Id));
            }
            db.Deleteable<UserDetail>().Where(x => list.Contains(x.UserID.Value) && x.SiteID == query.SiteID).ExecuteCommand();
            int count = db.Deleteable<UserInfo>().Where(x => list.Contains(x.ID) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功！";
                //删除角色用户
                db.Deleteable<RoleUser>().Where(x => list.Contains(x.UserID.Value) && x.SiteID == query.SiteID).ExecuteCommand();
            }
            else
            {
                result.code = 0;
                result.msg = "删除用户信息(UserInfo)失败！";
            }
            return result;
        }

        /// <summary>
        /// 验证最后更改
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool ValidateLastChanged(UserInfoQuery query)
        {
            UserInfo ui = db.Queryable<UserInfo>().Where(x => x.SiteID == query.SiteID && x.ID == query.ID).First();
            if (ui.LastChanged == query.lastChanged)
                return true;
            else
                return false;
        }

    }
}
