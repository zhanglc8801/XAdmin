using XAdmin.SqlSugar;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XAdmin.Repository
{
    public class SystemManagementRepository: ISystemManagementRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        #region 菜单管理
        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Menu> GetMenuList(QueryBase query)
        {
            return db.Queryable<Menu>().Where(x => x.SiteID == query.SiteID).OrderBy("SortID ASC").ToList();
        }

        /// <summary>
        /// 获取含有角色的所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<RoleMenuModel> GetSysMenuListWithRole(QueryBase query)
        {
            RoleMenuEx list = new RoleMenuEx();
            string sql1 = @"SELECT a.SiteID, a.MenuID as id,a.PMenuID as pId,a.MenuName as name,MenuUrl,Icon,IconColor,b.AuthStr,b.RoleID
                            FROM Menu a 
                            LEFT join RoleMenu b on a.MenuID=b.MenuID AND b.RoleID={0}
                            WHERE Status=1 AND a.SiteID='"+query.SiteID+"'";
            sql1 = string.Format(sql1, query.Int32Key);
            list.menuList = db.Ado.SqlQuery<RoleMenuModel>(sql1);
            string sql2 = "select a.*,case when b.MenuID is null then 'false' else 'true' end as CHECKED from Menu a left join RoleMenu b on a.MenuID=b.MenuID where a.SiteID='" + query.SiteID + "' and b.RoleID='" + query.Int32Key + "'";
            sql2 = "select a.MenuID as 'id' from Menu a INNER join RoleMenu b on a.MenuID = b.MenuID AND b.RoleID = " + query.Int32Key;
            list.menuAnyList = db.Ado.SqlQuery<RoleMenuModel>(sql2);
            return list.menuList;
        }

        /// <summary>
        /// 获取菜单实体
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public Menu GetMenuModel(MenuQuery query)
        {
            return db.Queryable<Menu>().Where(x => x.SiteID == query.SiteID && x.MenuID == query.MenuID).First();
        }

        #region 异步更新菜单数据
        /// <summary>
        /// 异步更新菜单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateMenuAsync(MenuQuery model)
        {
            ExecResult result = new ExecResult();
            int count = 0;
            if (model.UpdateField == "MenuName")
                count = await db.Updateable<Menu>().SetColumns(x => new Menu { MenuName = model.UpdateFieldValue }).Where(x => x.SiteID == model.SiteID && x.MenuID == model.MenuID).ExecuteCommandAsync();
            if (model.UpdateField == "MenuUrl")
                count = await db.Updateable<Menu>().SetColumns(x => new Menu { MenuUrl = model.UpdateFieldValue }).Where(x => x.SiteID == model.SiteID && x.MenuID == model.MenuID).ExecuteCommandAsync();
            if (model.UpdateField == "SortID")
                count = await db.Updateable<Menu>().SetColumns(x => new Menu { SortID = int.Parse(model.UpdateFieldValue) }).Where(x => x.SiteID == model.SiteID && x.MenuID == model.MenuID).ExecuteCommandAsync();

            if (count > 0)
            {
                result.code = 1;
                result.msg = "操作成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "操作失败！";
            }
            return result;
        } 
        #endregion

        /// <summary>
        /// 组装树形菜单下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeSelectModel> MakeTreeSelect(QueryBase query)
        {
            List<Menu> menuList = await db.Queryable<Menu>().Where(x => x.SiteID == query.SiteID).OrderBy("SortID ASC").ToListAsync();
            TreeSelectModel model = new TreeSelectModel();

            Menu menu = new Menu();
            menu.MenuID = 0;
            model.TreeSelectList = InitTreeSelect(menuList,menu);
            return model;
        }

        #region 生成树形菜单下拉框

        private List<TreeSelectItem> InitTreeSelect(List<Menu> menuList, Menu menu)
        {
            List<TreeSelectItem> rootNodeList = new List<TreeSelectItem>();

            var list = menuList.Where(x => x.PMenuID == menu.MenuID).ToList();
            foreach (Menu item in list)
            {
                TreeSelectItem treeItem = new TreeSelectItem();
                treeItem.id = item.MenuID;
                treeItem.name = item.MenuName;
                treeItem.open = false;
                treeItem.@checked = false;
                treeItem.children = InitTreeSelect(menuList, item);
                rootNodeList.Add(treeItem);
            }
            return rootNodeList;
        }
        #endregion

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveMenu(Menu model)
        {
            ExecResult result = new ExecResult();
            int count = 0;
            if (model.MenuID == 0)//添加菜单
                count = db.Insertable<Menu>(model).IgnoreColumns(new string[] { "MenuID" }).ExecuteCommand();
            else
                count = db.Updateable<Menu>(model).IgnoreColumns(new string[] { "AddDate" }).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "修改菜单成功！";
                if (model.MenuID == 0)
                    result.msg = "新增菜单成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "修改菜单失败！";
            }
            return result;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteMenu(QueryBase query)
        {
            ExecResult result = new ExecResult();
            List<long> list = new List<long>();
            string[] Ids = query.KeyStr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (string Id in Ids)
            {
                list.Add(int.Parse(Id));
            }
            int count = db.Deleteable<Menu>().Where(x => list.Contains(x.MenuID) && x.SiteID == query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "删除菜单信息(Menu)失败！";
            }
            return result;
        }
        #endregion

        #region 行政区划管理

        #region 异步获取所有行政区划列表
        /// <summary>
        /// 异步获取所有行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<AreaModel> GetAreaList(QueryBase query)
        {
            AreaModel am = new AreaModel();
            var areaList = db.Queryable<Area>().Where(x => x.ParentID > 0 && x.ParentID != 1610);
            if (query.Int32Key > 0)
                areaList = areaList.Where(x => x.ParentID == query.Int32Key);
            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                areaList = areaList.Where(x => x.AreaName.Contains(query.KeyStr));
            am.AreaList = await areaList.OrderBy("SNumber ASC").Select(x => new AreaEx
            {
                ID = x.ID,
                AreaCode = x.AreaCode,
                AreaName = x.AreaName,
                AreaLevel = x.AreaLevel,
                ParentID = x.ParentID,
                SNumber = x.SNumber,
                ShortName = x.ShortName,
                Status = x.Status,
                Remarks = x.Remarks
            }).ToListAsync();
            return am;
        } 
        #endregion

        #region 异步获取行政区划分页列表
        /// <summary>
        /// 异步获取行政区划分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<AreaModel> GetAreaPageList(QueryBase query)
        {
            AreaModel model = new AreaModel();
            RefAsync<int> TotalNumber = 0;
            var areaList = db.Queryable<Area>().Where(x => x.ParentID > 0 && x.ParentID != 1610);
            if (query.Int32Key > 0)
                areaList = areaList.Where(x => x.ParentID == query.Int32Key);
            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                areaList = areaList.Where(x => x.AreaName.Contains(query.KeyStr));
            model.AreaList = await areaList.OrderBy("ID,SNumber ASC").Select(x => new AreaEx
            {
                ID = x.ID,
                AreaCode = x.AreaCode,
                AreaName = x.AreaName,
                AreaLevel = x.AreaLevel,
                ParentID = x.ParentID,
                SNumber = x.SNumber,
                ShortName = x.ShortName,
                Status = x.Status,
                Remarks = x.Remarks
            }).ToPageListAsync(query.CurrentPage, query.PageSize, TotalNumber);
            model.PageSize = query.PageSize;
            model.TotalRecords = TotalNumber;
            return model;
        } 
        #endregion

        #region 异步更新行政区划信息
        /// <summary>
        /// 异步更新行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateAreaAsync(QueryBase model)
        {
            ExecResult result = new ExecResult();
            int count = 0;
            if (model.UpdateField == "AreaCode")
                count = await db.Updateable<Area>().SetColumns(x => new Area { AreaCode = model.UpdateFieldValue }).Where(x => x.ID == model.IntValue).ExecuteCommandAsync();
            if (model.UpdateField == "AreaName")
                count = await db.Updateable<Area>().SetColumns(x => new Area { AreaName = model.UpdateFieldValue }).Where(x => x.ID == model.IntValue).ExecuteCommandAsync();
            if (model.UpdateField == "SNumber")
                count = await db.Updateable<Area>().SetColumns(x => new Area { SNumber = Convert.ToInt32(model.UpdateFieldValue) }).Where(x => x.ID == model.IntValue).ExecuteCommandAsync();

            if (count > 0)
            {
                result.code = 1;
                result.msg = "操作成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "操作失败！";
            }
            return result;
        } 
        #endregion

        #region 删除行政区划信息
        /// <summary>
        /// 删除行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> DeleteAreaAsync(Area model)
        {
            ExecResult result = new ExecResult();
            int count = 0;
            count = await db.Deleteable<Area>().Where(x => x.ID == model.ID || x.ParentID == model.ID).ExecuteCommandAsync();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "操作成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "操作失败！";
            }
            return result;
        } 
        #endregion

        #region 设置行政区划禁用状态
        /// <summary>
        /// 设置行政区划禁用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> SetAreaStatusAsync(Area model)
        {
            ExecResult result = new ExecResult();
            int count = 0;
            count = await db.Updateable<Area>().SetColumns(x => x.Status == model.Status).Where(x => x.ID == model.ID || x.ParentID == model.ID).ExecuteCommandAsync();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "操作成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "操作失败！";
            }
            return result;
        } 
        #endregion

        #region 保存行政区划修改
        /// <summary>
        /// 保存行政区划修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveArea(Area model)
        {
            ExecResult result = new ExecResult();
            int count = 0;
            if (model.ID == 0)//添加
                count = db.Insertable<Area>(model).IgnoreColumns(new string[] { "ID" }).ExecuteCommand();
            else
                count = db.Updateable<Area>(model).IgnoreColumns(new string[] { "ShortName" }).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "修改成功！";
            }
            else
            {
                result.code = 0;
                result.msg = "修改失败！";
            }
            return result;
        } 
        #endregion

        #endregion
    }
}
