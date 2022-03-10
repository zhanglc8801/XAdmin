using XAdmin.SqlSugar;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XAdmin.IRepository;

namespace XAdmin.Repository
{
    /// <summary>
    /// 资源数据操作类
    /// </summary>
    public class ResourcesRepository: IResourcesRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        #region 插入资源表数据
        /// <summary>
        /// 插入资源表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult InsertResources(Resources model)
        {
            ExecResult result = new ExecResult();
            try
            {
                long i = db.Insertable<Resources>(model).ExecuteReturnBigIdentity();
                if (i > 0)
                {
                    result.msg = "操作成功。";
                    result.code = 1;
                    result.ReturnInt64 = i;
                }
                else
                {
                    result.msg = "操作失败。";
                    result.code = 0;
                }
            }
            catch (Exception ex)
            {
                result.msg = ex.Message;
                result.code = -1;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 获取所有资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ResourcesModel> GetAllResourcesPageList(ResourcesQuery query)
        {
            ResourcesModel model = new ResourcesModel();
            RefAsync<int> totalNumber = 0;
            model.ResourcesList = await db.Queryable<Resources>().Where(x => x.SiteID == query.SiteID).ToPageListAsync(query.CurrentPage, query.PageSize, totalNumber);
            model.TotalRecords = totalNumber;
            return model;
        }

        /// <summary>
        /// 根据条件获取资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ResourcesModel> GetResourcesPageList(ResourcesQuery query)
        {
            ResourcesModel model = new ResourcesModel();
            RefAsync<int> totalNumber = 0;
            var where = db.Queryable<Resources>().Where(x => x.SiteID == query.SiteID);
            if (!string.IsNullOrWhiteSpace(query.IDs))
                where = where.Where(x => query.IDs.Contains(x.ID.ToString()));
            if (!string.IsNullOrWhiteSpace(query.FileName))
                where = where.Where(x => x.FileName.Contains(query.FileName));
            if (query.StartTime != null)
                where = where.Where(x => x.UploadDate.Value >= query.StartTime.Value);
            if (query.EndTime != null)
                where = where.Where(x => x.UploadDate.Value < query.EndTime.Value);

            model.ResourcesList = await where.ToPageListAsync(query.CurrentPage, query.PageSize, totalNumber);
            model.TotalRecords = totalNumber;
            return model;
        }

        /// <summary>
        /// 根据ID字符串获取文件列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Resources>> GetResourcesList(ResourcesQuery query)
        {
            return await db.Queryable<Resources>().Where(x => x.SiteID == query.SiteID && query.IDs.Contains(x.ID.ToString())).ToListAsync();
        }

        /// <summary>
        /// 根据文件ID获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Resources GetResourcesModel(ResourcesQuery query)
        {
            return db.Queryable<Resources>().Where(x => x.SiteID == query.SiteID && x.ID == query.ID).First();
        }

        /// <summary>
        /// 根据文件MD5获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Resources GetResourcesModelByMD5(ResourcesQuery query)
        {
            return db.Queryable<Resources>().Where(x => x.SiteID == query.SiteID && x.FileMD5 == query.FileMD5  && x.UserId==query.UserId).First();
        }

        ///// <summary>
        ///// 删除资源表数据 标记删除状态
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public ExecResult DeleteResources(Resources model)
        //{
        //    ExecResult result = new ExecResult();

        //    var del = db.Updateable<Resources>().UpdateColumns("IsDeleted=1").Where(x => x.SiteID == model.SiteID && x.UserId == model.UserId);
        //    if (!string.IsNullOrWhiteSpace(model.FileMD5))
        //    {
        //        del.Where(x => x.FileMD5 == model.FileMD5).ExecuteCommand();
        //    }

        //    return result;

        //}

    }
}
