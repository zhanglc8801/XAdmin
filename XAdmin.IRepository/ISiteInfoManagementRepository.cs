using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.IRepository
{
    /// <summary>
    /// 网站信息管理接口类
    /// </summary>
    public interface ISiteInfoManagementRepository
    {
        #region 栏目管理
        /// <summary>
        /// 获取站点栏目列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<List<Site_Channel>> GetSiteChannelList(Site_ChannelQuery query);

        /// <summary>
        /// 递归获取当前栏目及其上级栏目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ChannelModel GetChannelList(Site_ChannelQuery query);

        /// <summary>
        /// 获取栏目实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<Site_Channel> GetSiteChannelModel(Site_ChannelQuery query);

        /// <summary>
        /// 组装上级栏目下拉框
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TreeSelectModel> MakeChannelSelect(QueryBase query);

        /// <summary>
        /// 更改栏目状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ExecResult> ChangeChannelStatus(Site_Channel model);

        /// <summary>
        /// 保存网站栏目信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveChannel(Site_Channel model);

        /// <summary>
        /// 获取栏目内容管理 左侧栏目菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<ChannelMenuModel> GetChannelMenuList(QueryBase query); 
        #endregion

        /// <summary>
        /// 更改各栏目内容状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ExecResult> ChangeChannelContentStatus(SiteChannelQuery query);

        #region 描述类
        /// <summary>
        /// 获取描述类内容实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        SiteChannel_DescriptionContent GetDescriptionModel(SiteChannelQuery query);

        /// <summary>
        /// 保存描述类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveDescription(SiteChannel_DescriptionContent model); 
        #endregion

        #region 新闻资讯类
        /// <summary>
        /// 获取新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<NewsContentModel> GetNewsPageList(SiteChannelQuery query);

        /// <summary>
        /// 获取站点新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<SiteChannel_NewsContent> GetSiteNewsPageList(SiteChannelQuery query);

        /// <summary>
        /// 获取新闻资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        SiteChannel_NewsContent GetNewsModel(SiteChannelQuery query);

        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveNewsInfo(SiteChannel_NewsContent model);

        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteNews(QueryBase query); 
        #endregion

        #region 图片类
        /// <summary>
        /// 获取图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ImageContentModel> GetImagePageList(SiteChannelQuery query);

        /// <summary>
        /// 获取站点图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ImageContentModel GetSiteImagePageList(SiteChannelQuery query);

        /// <summary>
        /// 获取图片资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        SiteChannel_ImageContent GetImageModel(SiteChannelQuery query);

        /// <summary>
        /// 保存图片资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveImageInfo(SiteChannel_ImageContent model);

        /// <summary>
        /// 删除图片资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteImage(QueryBase query);
        #endregion

        #region 视频类
        /// <summary>
        /// 获取视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<VideoContentModel> GetVideoPageList(SiteChannelQuery query);

        /// <summary>
        /// 获取站点视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IList<SiteChannel_VideoContent> GetSiteVideoPageList(SiteChannelQuery query);

        /// <summary>
        /// 获取视频内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        SiteChannel_VideoContent GetVideoModel(SiteChannelQuery query);

        /// <summary>
        /// 保存视频
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveVideoInfo(SiteChannel_VideoContent model);

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteVideo(QueryBase query);
        #endregion

        #region 网站内容预定
        /// <summary>
        /// 获取预定分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        YuDingModel GetYuDingPageList(YuDingQuery query);

        /// <summary>
        /// 获取预定内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        YuDing GetYuDingModel(YuDingQuery query);

        /// <summary>
        /// 保存预定信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveYuDing(YuDing model);

        /// <summary>
        /// 设置预定信息状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SetYuDingStatus(YuDing model);

        /// <summary>
        /// 删除预定信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteYuDing(QueryBase query); 
        #endregion

    }
}
