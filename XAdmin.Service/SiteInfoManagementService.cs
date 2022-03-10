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
    public class SiteInfoManagementService:ServiceBase, ISiteInfoManagementService
    {
        #region 栏目管理
        /// <summary>
        /// 获取站点栏目列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Site_Channel>> GetSiteChannelList(Site_ChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<Site_Channel> list = await clientHelper.PostAuthAsync<List<Site_Channel>, Site_ChannelQuery>(GetAPIUrl(APIConstants.GetSiteChannelList), query);
            return list;
        }

        /// <summary>
        /// 递归获取当前栏目及其上级栏目
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ChannelModel GetChannelList(Site_ChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ChannelModel model = clientHelper.PostAuth<ChannelModel, Site_ChannelQuery>(GetAPIUrl(APIConstants.GetChannelList), query);
            return model;
        }

        /// <summary>
        /// 获取栏目实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<Site_Channel> GetSiteChannelModel(Site_ChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            Site_Channel model = await clientHelper.PostAuthAsync<Site_Channel, Site_ChannelQuery>(GetAPIUrl(APIConstants.GetSiteChannelModel), query);
            return model;
        }

        /// <summary>
        /// 组装上级栏目下拉框
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeSelectModel> MakeChannelSelect(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            TreeSelectModel model = await clientHelper.PostAuthAsync<TreeSelectModel, QueryBase>(GetAPIUrl(APIConstants.MakeChannelSelect), query);
            return model;
        }

        /// <summary>
        /// 更改栏目状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeChannelStatus(Site_Channel model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, Site_Channel>(GetAPIUrl(APIConstants.ChangeChannelStatus), model);
            return result;
        }

        /// <summary>
        /// 保存网站栏目信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveChannel(Site_Channel model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, Site_Channel>(GetAPIUrl(APIConstants.SaveChannel), model);
            return result;
        }

        /// <summary>
        /// 获取栏目内容管理 左侧栏目菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<ChannelMenuModel> GetChannelMenuList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            IList<ChannelMenuModel> list = clientHelper.PostAuth<IList<ChannelMenuModel>, QueryBase>(GetAPIUrl(APIConstants.GetChannelMenuList), query);
            return list;
        } 
        #endregion

        /// <summary>
        /// 更改各栏目内容状态
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ExecResult> ChangeChannelContentStatus(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = await clientHelper.PostAuthAsync<ExecResult, SiteChannelQuery>(GetAPIUrl(APIConstants.ChangeChannelContentStatus), query);
            return result;
        }

        #region 描述类
        /// <summary>
        /// 获取描述类内容实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SiteChannel_DescriptionContent GetDescriptionModel(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            SiteChannel_DescriptionContent desc = clientHelper.PostAuth<SiteChannel_DescriptionContent, SiteChannelQuery>(GetAPIUrl(APIConstants.GetDescriptionModel), query);
            return desc;
        }

        /// <summary>
        /// 保存描述类信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveDescription(SiteChannel_DescriptionContent model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, SiteChannel_DescriptionContent>(GetAPIUrl(APIConstants.SaveDescription), model);
            return result;
        } 
        #endregion

        #region 新闻资讯类
        /// <summary>
        /// 获取新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<NewsContentModel> GetNewsPageList(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            NewsContentModel model = await clientHelper.PostAuthAsync<NewsContentModel, QueryBase>(GetAPIUrl(APIConstants.GetNewsPageList), query);
            return model;
        }

        /// <summary>
        /// 获取站点新闻资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<SiteChannel_NewsContent> GetSiteNewsPageList(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<SiteChannel_NewsContent> list = clientHelper.PostAuth<List<SiteChannel_NewsContent>, SiteChannelQuery>(GetAPIUrl(APIConstants.GetSiteNewsPageList), query);
            return list;
        }

        /// <summary>
        /// 获取新闻资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteChannel_NewsContent GetNewsModel(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            SiteChannel_NewsContent model = clientHelper.PostAuth<SiteChannel_NewsContent, SiteChannelQuery>(GetAPIUrl(APIConstants.GetNewsModel), query);
            return model;
        }

        /// <summary>
        /// 保存新闻资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveNewsInfo(SiteChannel_NewsContent model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, SiteChannel_NewsContent>(GetAPIUrl(APIConstants.SaveNewsInfo), model);
            return result;
        }

        /// <summary>
        /// 删除新闻资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteNews(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteNews), query);
            return result;
        } 
        #endregion

        #region 图片类
        /// <summary>
        /// 获取图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ImageContentModel> GetImagePageList(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ImageContentModel model = await clientHelper.PostAuthAsync<ImageContentModel, QueryBase>(GetAPIUrl(APIConstants.GetImagePageList), query);
            return model;
        }

        /// <summary>
        /// 获取站点图片资讯分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ImageContentModel GetSiteImagePageList(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ImageContentModel model = clientHelper.PostAuth<ImageContentModel, SiteChannelQuery>(GetAPIUrl(APIConstants.GetSiteImagePageList), query);
            return model;
        }

        /// <summary>
        /// 获取图片资讯内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteChannel_ImageContent GetImageModel(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            SiteChannel_ImageContent model = clientHelper.PostAuth<SiteChannel_ImageContent, SiteChannelQuery>(GetAPIUrl(APIConstants.GetImageModel), query);
            return model;
        }

        /// <summary>
        /// 保存图片资讯信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveImageInfo(SiteChannel_ImageContent model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, SiteChannel_ImageContent>(GetAPIUrl(APIConstants.SaveImageInfo), model);
            return result;
        }

        /// <summary>
        /// 删除图片资讯
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteImage(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteImage), query);
            return result;
        }
        #endregion

        #region 视频类
        /// <summary>
        /// 获取视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<VideoContentModel> GetVideoPageList(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            VideoContentModel model = await clientHelper.PostAuthAsync<VideoContentModel, QueryBase>(GetAPIUrl(APIConstants.GetVideoPageList), query);
            return model;
        }

        /// <summary>
        /// 获取站点视频分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IList<SiteChannel_VideoContent> GetSiteVideoPageList(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<SiteChannel_VideoContent> list = clientHelper.PostAuth<List<SiteChannel_VideoContent>, SiteChannelQuery>(GetAPIUrl(APIConstants.GetSiteVideoPageList), query);
            return list;
        }

        /// <summary>
        /// 获取视频内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteChannel_VideoContent GetVideoModel(SiteChannelQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            SiteChannel_VideoContent model = clientHelper.PostAuth<SiteChannel_VideoContent, SiteChannelQuery>(GetAPIUrl(APIConstants.GetVideoModel), query);
            return model;
        }

        /// <summary>
        /// 保存视频
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveVideoInfo(SiteChannel_VideoContent model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, SiteChannel_VideoContent>(GetAPIUrl(APIConstants.SaveVideoInfo), model);
            return result;
        }

        /// <summary>
        /// 删除视频
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteVideo(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteVideo), query);
            return result;
        }
        #endregion

        #region 网站内容预定
        /// <summary>
        /// 获取预定分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public YuDingModel GetYuDingPageList(YuDingQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            YuDingModel model = clientHelper.PostAuth<YuDingModel, YuDingQuery>(GetAPIUrl(APIConstants.GetYuDingPageList), query);
            return model;
        }

        /// <summary>
        /// 获取预定内容实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public YuDing GetYuDingModel(YuDingQuery query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            YuDing model = clientHelper.PostAuth<YuDing, YuDingQuery>(GetAPIUrl(APIConstants.GetYuDingModel), query);
            return model;
        }

        /// <summary>
        /// 保存预定信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveYuDing(YuDing model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, YuDing>(GetAPIUrl(APIConstants.SaveYuDing), model);
            return result;
        }

        /// <summary>
        /// 设置预定信息状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SetYuDingStatus(YuDing model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, YuDing>(GetAPIUrl(APIConstants.SetYuDingStatus), model);
            return result;
        }

        /// <summary>
        /// 删除预定信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteYuDing(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteYuDing), query);
            return result;
        }
        #endregion

    }
}
