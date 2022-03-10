using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XAdmin.IService
{
    public interface IGroupInfoService
    {
        /// <summary>
        /// 获取用户组分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GroupInfoListModel GetGroupInfoPageList(QueryBase query);

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IList<GroupInfo>> GetGroupInfoList(QueryBase query);

        /// <summary>
        /// 获取用户组信息
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        GroupInfo GetUserGroupModel(QueryBase query);

        /// <summary>
        /// 更改用户组状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ExecResult> ChangeGroupStatus(QueryBase query);

        /// <summary>
        /// 保存用户组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveGroupInfo(GroupInfo model);

        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteGroupInfo(QueryBase query);

        /// <summary>
        /// 获取用户组中的用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GroupUserEx GetGroupUserList(GroupUserQuery query);

        /// <summary>
        /// 从用户组中移除成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult RemoveGroupUser(QueryBase query);

        /// <summary>
        /// 获取未加入用户组的成员列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        GroupUserEx GetNotGroupUserList(GroupUserQuery query);

        /// <summary>
        /// 添加用户组成员
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult AddGroupUser(AddGroupUser query);
    }
}
