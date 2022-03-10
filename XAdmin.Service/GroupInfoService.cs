using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XAdmin.Service
{
    public class GroupInfoService: ServiceBase, IGroupInfoService
    {
        /// <summary>
        /// 获取用户组分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public GroupInfoListModel GetGroupInfoPageList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            GroupInfoListModel model = clientHelper.PostAuth<GroupInfoListModel, QueryBase>(GetAPIUrl(APIConstants.GetGroupInfoPageList), query);
            return model;
        }
        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IList<GroupInfo>> GetGroupInfoList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            IList<GroupInfo> GroupList = await clientHelper.PostAuthAsync<IList<GroupInfo>, QueryBase>(GetAPIUrl(APIConstants.GetGroupInfoList), query);
            return GroupList;
        }

        /// <summary>
        /// 获取用户组信息
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public GroupInfo GetUserGroupModel(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            GroupInfo UGroup = clientHelper.PostAuth<GroupInfo, QueryBase>(GetAPIUrl(APIConstants.GetUserGroupModel), query);
            return UGroup;
        }

        /// <summary>
        /// 更改用户组状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeGroupStatus(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, QueryBase>(GetAPIUrl(APIConstants.ChangeGroupStatus), query);
            return result;
        }

        /// <summary>
        /// 保存用户组信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveGroupInfo(GroupInfo model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, GroupInfo>(GetAPIUrl(APIConstants.SaveGroupInfo), model);
            return result;
        }

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteGroupInfo(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteGroupInfo), query);
            return result;
        }

        /// <summary>
        /// 获取用户组中的用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public GroupUserEx GetGroupUserList(GroupUserQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            GroupUserEx gUserList = clientHelper.PostAuth<GroupUserEx, GroupUserQuery>(GetAPIUrl(APIConstants.GetGroupUserList), query);
            return gUserList;
        }

        /// <summary>
        /// 从用户组中移除成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult RemoveGroupUser(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.RemoveGroupUser), query);
            return result;
        }

        /// <summary>
        /// 获取未加入用户组的成员列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public GroupUserEx GetNotGroupUserList(GroupUserQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            GroupUserEx gUserList = clientHelper.PostAuth<GroupUserEx, GroupUserQuery>(GetAPIUrl(APIConstants.GetNotGroupUserList), query);
            return gUserList;
        }

        /// <summary>
        /// 添加用户组成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult AddGroupUser(AddGroupUser query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.AddGroupUser), query);
            return result;
        }
    }
}
