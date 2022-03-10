using XAdmin.SqlSugar;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XAdmin.IRepository;

namespace XAdmin.Repository
{
    public class GroupInfoRepository: IGroupInfoRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        /// <summary>
        /// 获取用户组分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public GroupInfoListModel GetGroupInfoPageList(QueryBase query)
        {
            GroupInfoListModel model = new GroupInfoListModel();
            int TotalNumber = 0;
            var where = db.Queryable<GroupInfo>().Where(x => x.SiteID == query.SiteID);
            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                where = where.Where(x => x.GroupName.Contains(query.KeyStr));
            model.GroupInfoList = where.OrderBy("AddDate DESC").ToPageList(query.CurrentPage, query.PageSize, ref TotalNumber);
            model.TotalRecords = TotalNumber;
            model.PageSize = query.PageSize;
            return model;
        }

        /// <summary>
        /// 获取所有用户组列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IList<GroupInfo>> GetGroupInfoList(QueryBase query)
        {
            IList<GroupInfo> list = await db.Queryable<GroupInfo>().Where(x => x.SiteID == query.SiteID).OrderBy(x => x.GroupName).ToListAsync();
            return list;
        }

        /// <summary>
        /// 获取用户组信息
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public GroupInfo GetUserGroupModel(QueryBase query)
        {
            GroupInfo model = db.Queryable<GroupInfo>().Where(x => x.ID == query.Int32Key).First();
            return model;
        }

        /// <summary>
        /// 更改用户组状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeGroupStatus(QueryBase query)
        {
            int i = await db.Updateable<GroupInfo>().SetColumns(x => new GroupInfo { Status = query.boolValue }).Where(x => x.SiteID == query.SiteID && x.ID == query.Int32Key).ExecuteCommandAsync();
            ExecResult result = new ExecResult();
            if (i > 0)
                result.code = 1;
            else
                result.code = 0;
            return result;
        }

        /// <summary>
        /// 保存用户组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveGroupInfo(GroupInfo model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<GroupInfo>(model).IgnoreColumns("ID").ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增用户组成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增用户组失败";
                }
            }
            else
            {
                int i = db.Updateable<GroupInfo>(model).IgnoreColumns(new string[] { "AddDate" }).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "修改用户组成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "修改用户组失败";
                }
            }
            return result;
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteGroupInfo(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<int> list = new List<int>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(int.Parse(Id));
            }
            int count = db.Deleteable<GroupInfo>().Where(x => list.Contains(x.ID) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "修改成功";
            }
            else
            {
                result.code = 0;
                result.msg = "修改失败";
            }
            return result;
        }

        /// <summary>
        /// 获取用户组中的用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public GroupUserEx GetGroupUserList(GroupUserQuery query)
        {
            GroupUserEx model = new GroupUserEx();
            var Page = db.Queryable<GroupUser, GroupInfo, UserInfo>((gu,gi,ui)=>new object[] { JoinType.Left, gu.SiteID == gi.SiteID && gu.GroupID==gi.ID,JoinType.Left,gu.SiteID==ui.SiteID && gu.UserID==ui.ID }).Where((gu, gi, ui)=>gu.SiteID==query.SiteID);

            if (query.GroupID > 0)
                Page = Page.Where((gu, gi, ui) => gu.GroupID == query.GroupID);
            if (!string.IsNullOrWhiteSpace(query.UserName))
                Page = Page.Where((gu, gi, ui) => ui.UserName.Contains(query.UserName));
            if (!string.IsNullOrWhiteSpace(query.RealName))
                Page = Page.Where((gu, gi, ui) => ui.RealName.Contains(query.RealName));
            if (!string.IsNullOrWhiteSpace(query.Mobile))
                Page = Page.Where((gu, gi, ui) => ui.Mobile.Contains(query.Mobile));

            var getAll = Page.Select((gu, gi, ui) => new GroupUserList
            {
                ID=gu.ID,
                GroupID = gu.GroupID,
                GroupName = gi.GroupName,
                UserName = ui.UserName,
                UserID = gu.UserID,
                RealName = ui.RealName,
                Mobile = ui.Mobile,
                EMail = ui.EMail,
                Gender = ui.Gender,
                Status = ui.Status
            });
            model.list = getAll.ToPageList(query.CurrentPage, query.PageSize);
            model.PageSize = query.PageSize;
            model.TotalRecords = Page.Count();
            return model;
        }

        /// <summary>
        /// 从用户组中移除成员
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult RemoveGroupUser(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<long> list = new List<long>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(int.Parse(Id));
            }
            int count = db.Deleteable<GroupUser>().Where(x => list.Contains(x.ID.Value) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "操作成功";
            }
            else
            {
                result.code = 0;
                result.msg = "操作失败";
            }
            return result;
        }

        /// <summary>
        /// 获取未加入用户组的成员列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public GroupUserEx GetNotGroupUserList(GroupUserQuery query)
        {
            GroupUserEx model = new GroupUserEx();
            int TotalNumber = 0;
            var Page = db.Queryable<UserInfo, RoleUser, RoleInfo>((u, ru, ri) => new object[] { JoinType.Inner, u.ID == ru.UserID, JoinType.Left, ru.RoleID == ri.ID });
            Page = Page.Where((u, ru, ri)=>SqlFunc.Subqueryable<GroupUser>().Where(x=>x.UserID==u.ID && x.GroupID==query.GroupID).NotAny() && u.UserType==1 && u.SiteID==query.SiteID);

            if (!string.IsNullOrWhiteSpace(query.UserName))
                Page = Page.Where((u, ru, ri) => u.UserName.Contains(query.UserName));
            if (!string.IsNullOrWhiteSpace(query.RealName))
                Page = Page.Where((u, ru, ri) => u.UserName.Contains(query.RealName));
            if (query.RoleId > 0)
                Page = Page.Where((u, ru, ri) => ru.RoleID == query.RoleId);

            var getAll = Page.OrderBy(u => u.ID).Select((u, ru, ri) => new UserInfoEx
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
            model.UserList = getAll.ToPageList(query.CurrentPage, query.PageSize, ref TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        }

        /// <summary>
        /// 添加用户组成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult AddGroupUser(AddGroupUser query)
        {
            ExecResult res = new ExecResult();
            var insertObjs = new List<GroupUser>();
            string[] users = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < users.Length; i++)
            {
                insertObjs.Add(new GroupUser
                {
                    SiteID = query.SiteID,
                    GroupID = query.GroupID,
                    UserID = Int64.Parse(users[i]),
                    AddDate = DateTime.Now
                }); ;
            }
            try
            {
                int result = db.Insertable<GroupUser>(insertObjs.ToArray()).ExecuteCommand();
                if (result > 0)
                {
                    res.code = 1;
                    res.msg = "加入用户组成员成功";
                }
                else
                {
                    res.code = 0;
                    res.msg = "加入用户组成员失败";
                }
            }
            catch (Exception ex)
            {

                res.code = -1;
                res.msg = ex.Message;
            }
            return res;
        }
    }
}
