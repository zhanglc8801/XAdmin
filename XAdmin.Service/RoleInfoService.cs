using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Service
{
    public class RoleInfoService: ServiceBase, IRoleInfoService
    {
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RoleInfoListModel GetRolePageList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            RoleInfoListModel model = clientHelper.PostAuth<RoleInfoListModel, QueryBase>(GetAPIUrl(APIConstants.GetRolePageList), query);
            return model;
        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<RoleInfo>> GetRoleList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<RoleInfo> result = await clientHelper.PostAuthAsync<List<RoleInfo>, QueryBase>(GetAPIUrl(APIConstants.GetRoleList), query);
            return result;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public RoleInfo GetRoleModel(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            RoleInfo role = clientHelper.PostAuth<RoleInfo, QueryBase>(GetAPIUrl(APIConstants.GetRoleModel), query);
            return role;
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveRoleInfo(RoleInfo model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, RoleInfo>(GetAPIUrl(APIConstants.SaveRoleInfo), model);
            return result;
        }

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveRoleMenuAuth(RoleAuthListModel model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, RoleAuthListModel>(GetAPIUrl(APIConstants.SaveRoleMenuAuth), model);
            return result;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteRoleInfo(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteRoleInfo), query);
            return result;
        }

        /// <summary>
        /// 设置用户角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> SetUserRole(SetUserRoleModel model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, SetUserRoleModel>(GetAPIUrl(APIConstants.SetUserRole), model);
            return result;
        }

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<RoleMenuModel> GetRoleMenuList(RoleMenuQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            IList<RoleMenuModel> menuList = clientHelper.PostAuth<IList<RoleMenuModel>, RoleMenuQuery>(GetAPIUrl(APIConstants.GetRoleMenuList), query);
            return menuList;
        }

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SetRoleMenu(SetRoleMenu model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, SetRoleMenu>(GetAPIUrl(APIConstants.SetRoleMenu), model);
            return result;
        }

    }
}
