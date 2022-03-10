using XAdmin.Common.Utils;
using XAdmin.SqlSugar;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XAdmin.Repository
{
    public class RoleInfoRepository: IRoleInfoRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        #region 获取角色分页列表
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RoleInfoListModel GetRolePageList(QueryBase query)
        {
            RoleInfoListModel model = new RoleInfoListModel();
            int TotalNumber = 0;
            var where = db.Queryable<RoleInfo>().Where(x => x.SiteID == query.SiteID);
            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                where = where.Where(x => x.RoleName.Contains(query.KeyStr));
            model.RoleInfoList = where.OrderBy("AddDate DESC").ToPageList(query.CurrentPage, query.PageSize, ref TotalNumber);
            model.TotalRecords = TotalNumber;
            model.PageSize = query.PageSize;
            return model;
        }
        #endregion

        #region 获取所有角色列表
        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<RoleInfo>> GetRoleList(QueryBase query)
        {
            return await db.Queryable<RoleInfo>().Where(x => x.SiteID == query.SiteID).OrderBy("AddDate ASC").ToListAsync();
        }
        #endregion

        #region 获取角色信息实体
        /// <summary>
        /// 获取角色信息实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RoleInfo GetRoleModel(QueryBase query)
        {
            return db.Queryable<RoleInfo>().Where(x => x.ID == query.Int32Key).First();
        } 
        #endregion

        #region 保存角色信息
        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveRoleInfo(RoleInfo model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<RoleInfo>(model).IgnoreColumns("ID").ExecuteReturnIdentity();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增角色成功";
                    result.ReturnInt = i;
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增角色失败";
                }
            }
            else
            {
                int i = db.Updateable<RoleInfo>(model).IgnoreColumns(new string[] { "AddDate" }).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "修改角色成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "修改角色失败";
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveRoleMenuAuth(RoleAuthListModel model)
        {
            ExecResult result = new ExecResult();
            db.Deleteable<RoleMenu>().Where(x => x.RoleID == model.RoleID).ExecuteCommand();

            int i = db.Insertable<RoleMenu>(model.RoleAuthList).ExecuteCommand();
            if (i > 0)
            {
                result.code = 1;
                result.msg = "设置角色权限成功";
            }
            else
            {
                result.code = 0;
                result.msg = "设置角色权限失败";
            }
            return result;
        }

        #region 删除角色
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteRoleInfo(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<int> list = new List<int>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(int.Parse(Id));
            }
            int count = db.Deleteable<RoleInfo>().Where(x => list.Contains(x.ID) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除角色成功";
            }
            else
            {
                result.code = 0;
                result.msg = "删除角色失败";
            }
            return result;
        }
        #endregion

        #region 设置用户角色
        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> SetUserRole(SetUserRoleModel model)
        {
            ExecResult result = new ExecResult();
            string[] RoleIds = model.RoleIdStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            await db.Deleteable<RoleUser>().Where(x => x.SiteID == model.SiteID && x.UserID.Value == model.UserID).ExecuteCommandAsync();
            List<RoleUser> list = new List<RoleUser>();
            foreach (string item in RoleIds)
            {
                list.Add(new RoleUser
                {
                    SiteID = model.SiteID,
                    RoleID = int.Parse(item),
                    UserID = model.UserID,
                    AddDate = DateTime.Now
                });
            }
            int count = await db.Insertable<RoleUser>(list).ExecuteCommandAsync();
            if (count > 0)
            {
                result.code = 1;
            }
            else
            {
                result.code = 0;
                result.msg = "设置用户角色时出错。";
            }
            return result;
        }
        #endregion

        #region 获取角色菜单列表
        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<RoleMenuModel> GetRoleMenuList(RoleMenuQuery query)
        {
            RoleMenuEx list = new RoleMenuEx();
            string sql1 = "select * from Menu where SiteID='" + query.SiteID + "' and Status=1";
            list.menuList = db.Ado.SqlQuery<RoleMenuModel>(sql1);
            string sql2 = "select a.*,case when b.MenuID is null then 'false' else 'true' end as CHECKED from Menu a left join RoleMenu b on a.MenuID=b.MenuID where a.SiteID='" + query.SiteID + "' and b.RoleID='" + query.RoleID + "'";
            list.menuAnyList = db.Ado.SqlQuery<RoleMenuModel>(sql2);
            return list.menuList;
        } 
        #endregion

        #region 设置角色菜单
        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SetRoleMenu(SetRoleMenu model)
        {
            ExecResult result = new ExecResult();
            db.Deleteable<RoleMenu>().Where(x => x.RoleID == model.RoleID).ExecuteCommand();//删除原有的角色菜单
            List<RoleMenu> list = new List<RoleMenu>();
            string[] MenuIDs = model.MenuIDStr.Split(',');
            for (int i = 0; i < MenuIDs.Length; i++)
            {
                list.Add(new RoleMenu
                {
                    RMID = Guid.NewGuid().ToString(),
                    SiteID = model.SiteID,
                    RoleID = model.RoleID,
                    MenuID = Convert.ToInt32(MenuIDs[i]),
                    AddDate = DateTime.Now
                });
            }
            try
            {
                int count = db.Insertable<RoleMenu>(list).ExecuteCommand();
                if (count > 0)
                {
                    result.code = 1;
                    result.msg = "设置成功！";
                }
                else
                {
                    result.code = 0;
                    result.msg = "设置失败！";
                }
            }
            catch (Exception e)
            {

                result.code = -1;
                result.msg = e.Message;
            }
            return result;
        } 
        #endregion
    }
}
