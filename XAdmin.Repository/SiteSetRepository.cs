using XAdmin.SqlSugar;
using XAdmin.Model;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using SqlSugar;
using System;
using XAdmin.IRepository;
using XAdmin.Model.CustomEntity;

namespace XAdmin.Repository
{
    public class SiteSetRepository: ISiteSetRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();
        public bool InitSiteInfo(InitSiteInfoQuery query)
        {
            SugarParameter[] parms = db.Ado.GetParameters(new
            {
                @SiteID = DateTime.Now,
                @SiteName = "aaaaaaa",
                @SiteUrl = "11111",
                @SiteKey = "22222",
                @SiteDes = "33333"
            });
            int result = db.Ado.ExecuteCommand("insert into SiteInfo(SiteID,SiteName,SiteUrl,SiteKey,SiteDes) values(@SiteID,@SiteName,@SiteUrl,@SiteKey,@SiteDes)", parms);
            return true;
        }

        /// <summary>
        /// 获取站点配置信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SiteInfo GetSiteInfoModel(SiteInfoQuery query)
        {
            return db.Queryable<SiteInfo>().Where(x => x.SiteID == query.SiteID).First();
        }

        /// <summary>
        /// 保存站点配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveSiteInfo(SiteInfo model)
        {
            ExecResult result = new ExecResult();
            try
            {
                int i = db.Updateable<SiteInfo>(model).IgnoreColumns(new string[] { "AddDate" }).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "保存成功！";
                }
                else
                {
                    result.code = 0;
                    result.msg = "保存失败！";
                }
            }
            catch (Exception ex)
            {
                result.code = -1;
                result.msg = ex.Message;
            }
            return result;
        }
    }
}
