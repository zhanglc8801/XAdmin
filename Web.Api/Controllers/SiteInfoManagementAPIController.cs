using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAdmin.IRepository;

namespace Web.Api.Controllers
{
    /// <summary>
    /// 网站信息管理API
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SiteInfoManagementAPIController : ControllerBase
    {
        private ISiteInfoManagementRepository _siteInfo;
        public SiteInfoManagementAPIController(ISiteInfoManagementRepository siteInfo)
        {
            this._siteInfo = siteInfo;
        }

        #region 栏目管理
        /// <summary>
        /// 获取站点栏目列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Site_Channel>> GetSiteChannelList(Site_ChannelQuery query)
        {
            return await _siteInfo.GetSiteChannelList(query);
        }

        /// <summary>
        /// 递归获取当前栏目及其上级栏目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ChannelModel GetChannelList(Site_ChannelQuery query)
        {
            return _siteInfo.GetChannelList(query);
        }

        /// <summary>
        /// 获取栏目实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Site_Channel> GetSiteChannelModel(Site_ChannelQuery query)
        {
            return await _siteInfo.GetSiteChannelModel(query);
        }

        /// <summary>
        /// 组装上级栏目下拉框
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TreeSelectModel> MakeChannelSelect(QueryBase query)
        {
            return await _siteInfo.MakeChannelSelect(query);
        }

        /// <summary>
        /// 更改栏目状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> ChangeChannelStatus(Site_Channel model)
        {
            return await _siteInfo.ChangeChannelStatus(model);
        }

        /// <summary>
        /// 保存网站栏目信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveChannel(Site_Channel model)
        {
            return _siteInfo.SaveChannel(model);
        }

        /// <summary>
        /// 获取栏目内容管理 左侧栏目菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IList<ChannelMenuModel> GetChannelMenuList(QueryBase query)
        {
            return _siteInfo.GetChannelMenuList(query);
        } 
        #endregion

        /// <summary>
        /// 更改各栏目内容状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ExecResult> ChangeChannelContentStatus(SiteChannelQuery query)
        {
            return await _siteInfo.ChangeChannelContentStatus(query);
        }

        #region 描述类
        /// <summary>
        /// 获取描述类内容实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public SiteChannel_DescriptionContent GetDescriptionModel(SiteChannelQuery query)
        {
            return _siteInfo.GetDescriptionModel(query);
        }

        /// <summary>
        /// 保存描述类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveDescription(SiteChannel_DescriptionContent model)
        {
            return _siteInfo.SaveDescription(model);
        }
        #endregion

        #region 新闻资讯类
        /// <summary>
        /// 获取新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<NewsContentModel> GetNewsPageList(SiteChannelQuery query)
        {
            return await _siteInfo.GetNewsPageList(query);
        }

        /// <summary>
        /// 获取站点新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IList<SiteChannel_NewsContent> GetSiteNewsPageList(SiteChannelQuery query)
        {
            return _siteInfo.GetSiteNewsPageList(query);
        }

        /// <summary>
        /// 获取新闻资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public SiteChannel_NewsContent GetNewsModel(SiteChannelQuery query)
        {
            return _siteInfo.GetNewsModel(query);
        }

        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveNewsInfo(SiteChannel_NewsContent model)
        {
            return _siteInfo.SaveNewsInfo(model);
        }

        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteNews(QueryBase query)
        {
            return _siteInfo.DeleteNews(query);
        }
        #endregion

        #region 图片类
        /// <summary>
        /// 获取图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ImageContentModel> GetImagePageList(SiteChannelQuery query)
        {
            return await _siteInfo.GetImagePageList(query);
        }

        /// <summary>
        /// 获取站点图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public ImageContentModel GetSiteImagePageList(SiteChannelQuery query)
        {
            return _siteInfo.GetSiteImagePageList(query);
        }

        /// <summary>
        /// 获取图片资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public SiteChannel_ImageContent GetImageModel(SiteChannelQuery query)
        {
            return _siteInfo.GetImageModel(query);
        }

        /// <summary>
        /// 保存图片资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveImageInfo(SiteChannel_ImageContent model)
        {
            return _siteInfo.SaveImageInfo(model);
        }

        /// <summary>
        /// 删除图片资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteImage(QueryBase query)
        {
            return _siteInfo.DeleteImage(query);
        }
        #endregion

        #region 视频类
        /// <summary>
        /// 获取视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<VideoContentModel> GetVideoPageList(SiteChannelQuery query)
        {
            return await _siteInfo.GetVideoPageList(query);
        }

        /// <summary>
        /// 获取站点视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IList<SiteChannel_VideoContent> GetSiteVideoPageList(SiteChannelQuery query)
        {
            return _siteInfo.GetSiteVideoPageList(query);
        }

        /// <summary>
        /// 获取视频内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public SiteChannel_VideoContent GetVideoModel(SiteChannelQuery query)
        {
            return _siteInfo.GetVideoModel(query);
        }

        /// <summary>
        /// 保存视频
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveVideoInfo(SiteChannel_VideoContent model)
        {
            return _siteInfo.SaveVideoInfo(model);
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteVideo(QueryBase query)
        {
            return _siteInfo.DeleteVideo(query);
        }
        #endregion

        #region 网站内容预定
        /// <summary>
        /// 获取预定分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public YuDingModel GetYuDingPageList(YuDingQuery query)
        {
            return _siteInfo.GetYuDingPageList(query);
        }

        /// <summary>
        /// 获取预定内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public YuDing GetYuDingModel(YuDingQuery query)
        {
            return _siteInfo.GetYuDingModel(query);
        }

        /// <summary>
        /// 保存预定信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveYuDing(YuDing model)
        {
            return _siteInfo.SaveYuDing(model);
        }

        /// <summary>
        /// 设置预定信息状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SetYuDingStatus(YuDing model)
        {
            return _siteInfo.SetYuDingStatus(model);
        }

        /// <summary>
        /// 删除预定信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteYuDing(QueryBase query)
        {
            return _siteInfo.DeleteYuDing(query);
        }
        #endregion


    }
}
