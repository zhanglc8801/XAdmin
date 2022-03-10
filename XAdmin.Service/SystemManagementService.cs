using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Service
{
    public class SystemManagementService: ServiceBase, ISystemManagementService
    {
        #region 菜单管理
        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Menu> GetMenuList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<Menu> list = clientHelper.PostAuth<List<Menu>, QueryBase>(GetAPIUrl(APIConstants.GetMenuList), query);
            return list;
        }

        /// <summary>
        /// 获取含有角色的所有菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<RoleMenuModel> GetSysMenuListWithRole(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<RoleMenuModel> list = clientHelper.PostAuth<List<RoleMenuModel>, QueryBase>(GetAPIUrl(APIConstants.GetSysMenuListWithRole), query);
            return list;
        }

        /// <summary>
        /// 获取菜单实体
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public Menu GetMenuModel(MenuQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            Menu MenuModel = clientHelper.PostAuth<Menu, MenuQuery>(GetAPIUrl(APIConstants.GetMenuModel), query);
            return MenuModel;
        }

        /// <summary>
        /// 异步更新菜单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateMenuAsync(MenuQuery model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, MenuQuery>(GetAPIUrl(APIConstants.UpdateMenuAsync), model);
            return result;
        }

        /// <summary>
        /// 组装树形菜单下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeSelectModel> MakeTreeSelect(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            TreeSelectModel MenuModel = await clientHelper.PostAuthAsync<TreeSelectModel, QueryBase>(GetAPIUrl(APIConstants.MakeTreeSelect), query);
            return MenuModel;
        }

        /// <summary>
        /// 组装菜单图标下拉框IconSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeSelectModel> MakeIconSelect(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            TreeSelectModel MenuModel = await clientHelper.PostAuthAsync<TreeSelectModel, QueryBase>(GetAPIUrl(APIConstants.MakeIconSelect), query);
            return MenuModel;
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveMenu(Menu model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, Menu>(GetAPIUrl(APIConstants.SaveMenu), model);
            return result;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteMenu(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteMenu), query);
            return result;
        }
        #endregion

        #region 行政区划管理
        /// <summary>
        /// 异步获取所有行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<AreaModel> GetAreaList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            AreaModel model = await clientHelper.PostAuthAsync<AreaModel, QueryBase>(GetAPIUrl(APIConstants.GetAreaList), query);
            return model;
        }

        /// <summary>
        /// 异步获取行政区划分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<AreaModel> GetAreaPageList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            AreaModel model = await clientHelper.PostAuthAsync<AreaModel, QueryBase>(GetAPIUrl(APIConstants.GetAreaPageList), query);
            return model;
        }

        /// <summary>
        /// 异步更新行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> UpdateAreaAsync(QueryBase model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, QueryBase>(GetAPIUrl(APIConstants.UpdateAreaAsync), model);
            return result;
        }

        /// <summary>
        /// 删除行政区划信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> DeleteAreaAsync(Area model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, Area>(GetAPIUrl(APIConstants.DeleteAreaAsync), model);
            return result;
        }

        /// <summary>
        /// 设置行政区划禁用状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> SetAreaStatusAsync(Area model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, Area>(GetAPIUrl(APIConstants.SetAreaStatusAsync), model);
            return result;
        }

        /// <summary>
        /// 保存行政区划修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveArea(Area model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, Area>(GetAPIUrl(APIConstants.SaveArea), model);
            return result;
        }
        #endregion
    }
}
