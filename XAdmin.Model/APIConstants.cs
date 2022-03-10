using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Model
{
    public class APIConstants
    {
        /// <summary>
        /// 初始化站点信息
        /// </summary>
        public const string InitSiteInfo = "api/SiteSetAPI/InitSiteInfo";

        /// <summary>
        /// 获取站点配置信息
        /// </summary>
        public const string GetSiteInfo = "api/SiteSetAPI/GetSiteInfo";

        /// <summary>
        /// 保存站点配置信息
        /// </summary>
        public const string SaveSiteInfo = "api/SiteSetAPI/SaveSiteInfo";

        #region 登录相关
        /// <summary>
        /// 用户登录
        /// </summary>
        public const string UserLogin = "api/UserInfoAPI/UserLogin";

        /// <summary>
        /// 修改用户登录信息
        /// </summary>
        public const string UpdateLoginInfo = "api/UserInfoAPI/UpdateLoginInfo";

        /// <summary>
        /// 取用户角色ID列表
        /// </summary>
        public const string GetUserRoleIDList = "api/UserInfoAPI/GetUserRoleIDList";

        /// <summary>
        /// 更改密码
        /// </summary>
        public const string ChangePassword = "api/UserInfoAPI/ChangePassword";

        /// <summary>
        /// 设置新密码
        /// </summary>
        public const string SetNewPassword = "api/UserInfoAPI/SetNewPassword";
        #endregion

        #region 用户管理
        /// <summary>
        /// 获取用户列表
        /// </summary>
        public const string GetUserPageList = "api/UserInfoAPI/GetUserPageList";

        /// <summary>
        /// 更改用户状态
        /// </summary>
        public const string ChangeUserStatus = "api/UserInfoAPI/ChangeUserStatus";

        /// <summary>
        /// 更改用户头像
        /// </summary>
        public const string ChangeUserPhoto = "api/UserInfoAPI/ChangeUserPhoto";

        /// <summary>
        /// 删除用户信息
        /// </summary>
        public const string DeleteUserInfo = "api/UserInfoAPI/DeleteUserInfo";

        /// <summary>
        /// 验证最后更改
        /// </summary>
        public const string ValidateLastChanged = "api/UserInfoAPI/ValidateLastChanged";

        /// <summary>
        /// 获取用户信息实体
        /// </summary>
        public const string GetUserInfoAsync = "api/UserInfoAPI/GetUserModel";

        /// <summary>
        /// 修改用户基本信息
        /// </summary>
        public const string UpdateUserInfoAsync = "api/UserInfoAPI/UpdateUserInfo";

        /// <summary>
        /// 修改用户扩展信息
        /// </summary>
        public const string UpdateUserDetailAsync = "api/UserInfoAPI/UpdateUserDetail";
        #endregion

        #region 用户组管理
        /// <summary>
        /// 获取用户组分页列表
        /// </summary>
        public const string GetGroupInfoPageList = "api/RightMgrAPI/GetGroupInfoPageList";

        /// <summary>
        /// 获取用户组列表
        /// </summary>
        public const string GetGroupInfoList = "api/RightMgrAPI/GetGroupInfoList";

        /// <summary>
        /// 获取用户组信息
        /// </summary>
        public const string GetUserGroupModel = "api/RightMgrAPI/GetUserGroupModel";

        /// <summary>
        /// 更改用户组状态
        /// </summary>
        public const string ChangeGroupStatus = "api/RightMgrAPI/ChangeGroupStatus";

        /// <summary>
        /// 保存用户组信息
        /// </summary>
        public const string SaveGroupInfo = "api/RightMgrAPI/SaveGroupInfo";

        /// <summary>
        /// 删除用户组
        /// </summary>
        public const string DeleteGroupInfo = "api/RightMgrAPI/DeleteGroupInfo";

        /// <summary>
        /// 获取用户组中的用户列表
        /// </summary>
        public const string GetGroupUserList = "api/RightMgrAPI/GetGroupUserList";

        /// <summary>
        /// 从用户组中移除成员
        /// </summary>
        public const string RemoveGroupUser = "api/RightMgrAPI/RemoveGroupUser";

        /// <summary>
        /// 获取未加入用户组的成员列表
        /// </summary>
        public const string GetNotGroupUserList = "api/RightMgrAPI/GetNotGroupUserList";

        /// <summary>
        /// 添加用户组成员
        /// </summary>
        public const string AddGroupUser = "api/RightMgrAPI/AddGroupUser";
        #endregion

        #region 角色管理
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        public const string GetRolePageList = "api/RightMgrAPI/GetRolePageList";

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        public const string GetRoleList = "api/RightMgrAPI/GetRoleList";

        /// <summary>
        /// 获取角色信息
        /// </summary>
        public const string GetRoleModel = "api/RightMgrAPI/GetRoleModel";

        /// <summary>
        /// 保存角色信息
        /// </summary>
        public const string SaveRoleInfo = "api/RightMgrAPI/SaveRoleInfo";

        /// <summary>
        /// 保存角色菜单权限
        /// </summary>
        public const string SaveRoleMenuAuth = "api/RightMgrAPI/SaveRoleMenuAuth";

        /// <summary>
        /// 删除角色
        /// </summary>
        public const string DeleteRoleInfo = "api/RightMgrAPI/DeleteRoleInfo";

        /// <summary>
        /// 设置用户角色
        /// </summary>
        public const string SetUserRole = "api/RightMgrAPI/SetUserRole";

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        public const string GetRoleMenuList = "api/RightMgrAPI/GetRoleMenuList";

        /// <summary>
        /// 设置角色菜单
        /// </summary>
        public const string SetRoleMenu = "api/RightMgrAPI/SetRoleMenu";
        #endregion

        #region 菜单管理
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        public const string GetMenuList = "api/SystemManagementAPI/GetMenuList";

        /// <summary>
        /// 获取含有角色的所有菜单列表
        /// </summary>
        public const string GetSysMenuListWithRole = "api/SystemManagementAPI/GetSysMenuListWithRole";

        /// <summary>
        /// 获取菜单实体
        /// </summary>
        public const string GetMenuModel = "api/SystemManagementAPI/GetMenuModel";

        /// <summary>
        /// 异步更新菜单数据
        /// </summary>
        public const string UpdateMenuAsync = "api/SystemManagementAPI/UpdateMenu";

        /// <summary>
        /// 组装树形菜单下拉框TreeSelect
        /// </summary>
        public const string MakeTreeSelect = "api/SystemManagementAPI/MakeTreeSelect";

        /// <summary>
        /// 组装菜单图标下拉框IconSelect
        /// </summary>
        public const string MakeIconSelect = "api/SystemManagementAPI/MakeIconSelect";

        /// <summary>
        /// 保存菜单
        /// </summary>
        public const string SaveMenu = "api/SystemManagementAPI/SaveMenu";

        /// <summary>
        /// 删除菜单
        /// </summary>
        public const string DeleteMenu = "api/SystemManagementAPI/DeleteMenu";
        #endregion

        #region 行政区划管理
        /// <summary>
        /// 异步获取所有行政区划列表
        /// </summary>
        public const string GetAreaList = "api/SystemManagementAPI/GetAreaList";

        /// <summary>
        /// 异步获取行政区划分页列表
        /// </summary>
        public const string GetAreaPageList = "api/SystemManagementAPI/GetAreaPageList";

        /// <summary>
        /// 异步更新行政区划信息
        /// </summary>
        public const string UpdateAreaAsync = "api/SystemManagementAPI/UpdateArea";

        /// <summary>
        /// 删除行政区划信息
        /// </summary>
        public const string DeleteAreaAsync = "api/SystemManagementAPI/DeleteArea";

        /// <summary>
        /// 设置行政区划禁用状态
        /// </summary>
        public const string SetAreaStatusAsync = "api/SystemManagementAPI/SetAreaStatus";

        /// <summary>
        /// 保存行政区划修改
        /// </summary>
        public const string SaveArea = "api/SystemManagementAPI/SaveArea";
        #endregion

        /// <summary>
        /// 获取省级行政区划列表
        /// </summary>
        public const string GetProvinceList = "api/CommonAPI/GetProvinceList";

        /// <summary>
        /// 根据ParentID获取行政区划列表
        /// </summary>
        public const string GetAreaListByParentID = "api/CommonAPI/GetAreaListByParentID";

        /// <summary>
        /// 获取行政区划实体
        /// </summary>
        public const string GetAreaModel = "api/CommonAPI/GetAreaModel";

        /// <summary>
        /// 通用选择区域下拉框TreeSelect
        /// </summary>
        public const string MakeAreaTreeSelect = "api/CommonAPI/MakeAreaTreeSelect";

        #region 网站信息管理
        /// <summary>
        /// 获取站点栏目列表
        /// </summary>
        public const string GetSiteChannelList = "api/SiteInfoManagementAPI/GetSiteChannelList";

        /// <summary>
        /// 递归获取当前栏目及其上级栏目
        /// </summary>
        public const string GetChannelList = "api/SiteInfoManagementAPI/GetChannelList";

        /// <summary>
        /// 获取栏目实体
        /// </summary>
        public const string GetSiteChannelModel = "api/SiteInfoManagementAPI/GetSiteChannelModel";

        /// <summary>
        /// 组装上级栏目下拉框
        /// </summary>
        public const string MakeChannelSelect = "api/SiteInfoManagementAPI/MakeChannelSelect";

        /// <summary>
        /// 更改栏目状态
        /// </summary>
        public const string ChangeChannelStatus = "api/SiteInfoManagementAPI/ChangeChannelStatus";

        /// <summary>
        /// 保存网站栏目信息
        /// </summary>
        public const string SaveChannel = "api/SiteInfoManagementAPI/SaveChannel";

        /// <summary>
        /// 获取栏目内容管理 左侧栏目菜单
        /// </summary>
        public const string GetChannelMenuList = "api/SiteInfoManagementAPI/GetChannelMenuList";

        /// <summary>
        /// 更改各栏目内容状态
        /// </summary>
        public const string ChangeChannelContentStatus = "api/SiteInfoManagementAPI/ChangeChannelContentStatus";

        #region 描述类
        /// <summary>
        /// 获取描述类内容实体
        /// </summary>
        public const string GetDescriptionModel = "api/SiteInfoManagementAPI/GetDescriptionModel";

        /// <summary>
        /// 保存描述类信息
        /// </summary>
        public const string SaveDescription = "api/SiteInfoManagementAPI/SaveDescription"; 
        #endregion

        #region 新闻资讯类
        /// <summary>
        /// 获取新闻资讯分页列表
        /// </summary>
        public const string GetNewsPageList = "api/SiteInfoManagementAPI/GetNewsPageList";

        /// <summary>
        /// 获取站点新闻资讯分页列表
        /// </summary>
        public const string GetSiteNewsPageList = "api/SiteInfoManagementAPI/GetSiteNewsPageList";

        /// <summary>
        /// 获取新闻资讯内容实体
        /// </summary>
        public const string GetNewsModel = "api/SiteInfoManagementAPI/GetNewsModel";

        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        public const string SaveNewsInfo = "api/SiteInfoManagementAPI/SaveNewsInfo";

        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        public const string DeleteNews = "api/SiteInfoManagementAPI/DeleteNews"; 
        #endregion

        #region 图片类
        /// <summary>
        /// 获取图片资讯分页列表
        /// </summary>
        public const string GetImagePageList = "api/SiteInfoManagementAPI/GetImagePageList";

        /// <summary>
        /// 获取站点图片资讯分页列表
        /// </summary>
        public const string GetSiteImagePageList = "api/SiteInfoManagementAPI/GetSiteImagePageList";

        /// <summary>
        /// 获取图片资讯内容实体
        /// </summary>
        public const string GetImageModel = "api/SiteInfoManagementAPI/GetImageModel";

        /// <summary>
        /// 保存图片资讯信息
        /// </summary>
        public const string SaveImageInfo = "api/SiteInfoManagementAPI/SaveImageInfo";

        /// <summary>
        /// 删除图片资讯
        /// </summary>
        public const string DeleteImage = "api/SiteInfoManagementAPI/DeleteImage";
        #endregion

        #region 视频类
        /// <summary>
        /// 获取视频分页列表
        /// </summary>
        public const string GetVideoPageList = "api/SiteInfoManagementAPI/GetVideoPageList";

        /// <summary>
        /// 获取站点视频分页列表
        /// </summary>
        public const string GetSiteVideoPageList = "api/SiteInfoManagementAPI/GetSiteVideoPageList";

        /// <summary>
        /// 获取视频内容实体
        /// </summary>
        public const string GetVideoModel = "api/SiteInfoManagementAPI/GetVideoModel";

        /// <summary>
        /// 保存视频
        /// </summary>
        public const string SaveVideoInfo = "api/SiteInfoManagementAPI/SaveVideoInfo";

        /// <summary>
        /// 删除视频
        /// </summary>
        public const string DeleteVideo = "api/SiteInfoManagementAPI/DeleteVideo";
        #endregion

        #region 网站内容预定
        /// <summary>
        /// 获取预定分页列表
        /// </summary>
        public const string GetYuDingPageList = "api/SiteInfoManagementAPI/GetYuDingPageList";

        /// <summary>
        /// 获取预定内容实体
        /// </summary>
        public const string GetYuDingModel = "api/SiteInfoManagementAPI/GetYuDingModel";

        /// <summary>
        /// 保存预定信息
        /// </summary>
        public const string SaveYuDing = "api/SiteInfoManagementAPI/SaveYuDing";

        /// <summary>
        /// 设置预定信息状态
        /// </summary>
        public const string SetYuDingStatus = "api/SiteInfoManagementAPI/SetYuDingStatus";

        /// <summary>
        /// 删除预定信息
        /// </summary>
        public const string DeleteYuDing = "api/SiteInfoManagementAPI/DeleteYuDing"; 
        #endregion

        #endregion

        #region 资源文件管理
        /// <summary>
        /// 插入资源表数据
        /// </summary>
        public const string InsertResources = "api/ResourcesAPI/InsertResources";

        /// <summary>
        /// 获取所有资源分页列表
        /// </summary>
        public const string GetAllResourcesPageList = "api/ResourcesAPI/GetAllResourcesPageList";

        /// <summary>
        /// 根据条件获取资源分页列表
        /// </summary>
        public const string GetResourcesPageList = "api/ResourcesAPI/GetResourcesPageList";

        /// <summary>
        /// 根据ID字符串获取文件列表
        /// </summary>
        public const string GetResourcesList = "api/ResourcesAPI/GetResourcesList";

        /// <summary>
        /// 根据文件ID获取资源实体
        /// </summary>
        public const string GetResourcesModel = "api/ResourcesAPI/GetResourcesModel";

        /// <summary>
        /// 根据文件MD5获取资源实体
        /// </summary>
        public const string GetResourcesModelByMD5 = "api/ResourcesAPI/GetResourcesModelByMD5";
        #endregion

        #region 流程管理
        #region 表单类别
        /// <summary>
        /// 获取所有表单类别列表
        /// </summary>
        public const string GetFormTypeList = "api/WorkFlowAPI/GetFormTypeList";

        /// <summary>
        /// 获取表单类别实体
        /// </summary>
        public const string GetFormTypeModel = "api/WorkFlowAPI/GetFormTypeModel";

        /// <summary>
        /// 保存表单类别信息
        /// </summary>
        public const string SaveFormType = "api/WorkFlowAPI/SaveFormType";

        /// <summary>
        /// 删除表单类别
        /// </summary>
        public const string DeleteFormType = "api/WorkFlowAPI/DeleteFormType";
        #endregion

        #region 表单设计
        /// <summary>
        /// 获取所有表单列表
        /// </summary>
        public const string GetFormList = "api/WorkFlowAPI/GetFormList";

        /// <summary>
        /// 获取表单实体
        /// </summary>
        public const string GetFormModel = "api/WorkFlowAPI/GetFormModel";

        /// <summary>
        /// 保存表单信息
        /// </summary>
        public const string SaveForm = "api/WorkFlowAPI/SaveForm";

        /// <summary>
        /// 删除表单
        /// </summary>
        public const string DeleteForm = "api/WorkFlowAPI/DeleteForm";
        #endregion 
        #endregion
    }
}
