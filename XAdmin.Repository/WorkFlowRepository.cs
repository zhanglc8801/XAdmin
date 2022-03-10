using XAdmin.SqlSugar;
using XAdmin.Model;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.IRepository;
using XAdmin.Model.CustomEntity;

namespace XAdmin.Repository
{
    public class WorkFlowRepository:IWorkFlowRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        #region 表单类别
        /// <summary>
        /// 获取所有表单类别列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<FormType> GetFormTypeList(QueryBase query)
        {
            var data = db.Queryable<FormType>().Where(x => x.SiteID == query.SiteID);
            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                data = data.Where(x=>x.FormTypeName.Contains(query.KeyStr));
            return data.ToList();
        }

        /// <summary>
        /// 获取表单类别实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public FormType GetFormTypeModel(QueryBase query)
        {
            return db.Queryable<FormType>().Where(x => x.ID == query.Int32Key).First();
        }

        #region 保存表单类别信息
        /// <summary>
        /// 保存表单类别信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveFormType(FormType model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<FormType>(model).IgnoreColumns("ID").ExecuteReturnIdentity();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增表单类别成功";
                    result.ReturnInt = i;
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增表单类别失败";
                }
            }
            else
            {
                int i = db.Updateable<FormType>(model).IgnoreColumns("ID").Where(x => x.ID == model.ID).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "修改表单类别成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "修改表单类别失败";
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 删除表单类别
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteFormType(QueryBase query)
        {
            ExecResult result = new ExecResult();
            int count = db.Deleteable<FormType>().Where(x => x.ID == query.Int32Key && x.SiteID==query.SiteID).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功";
            }
            else
            {
                result.code = 0;
                result.msg = "删除失败";
            }
            return result;
        }
        #endregion

        #region 表单设计
        /// <summary>
        /// 获取所有表单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Form> GetFormList(QueryBase query)
        {
            var form = db.Queryable<Form>();
            if (query.Int32Key > 0)
                form = form.Where(x => x.FormType == query.Int32Key);
            if (!string.IsNullOrWhiteSpace(query.KeyStr))
                form = form.Where(x => x.FormName.Contains(query.KeyStr));
            return form.ToList();
        }

        /// <summary>
        /// 获取表单实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Form GetFormModel(QueryBase query)
        {
            return db.Queryable<Form>().Where(x => x.ID == query.Int32Key).First();
        }

        #region 保存表单信息
        /// <summary>
        /// 保存表单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveForm(Form model)
        {
            ExecResult result = new ExecResult();
            if (model.ID == 0)
            {
                int i = db.Insertable<Form>(model).IgnoreColumns("ID").ExecuteReturnIdentity();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "新增表单成功";
                    result.ReturnInt = i;
                }
                else
                {
                    result.code = 0;
                    result.msg = "新增表单失败";
                }
            }
            else
            {
                int i = db.Updateable<Form>(model).IgnoreColumns("ID").Where(x => x.ID == model.ID).ExecuteCommand();
                if (i > 0)
                {
                    result.code = 1;
                    result.msg = "修改表单成功";
                }
                else
                {
                    result.code = 0;
                    result.msg = "修改表单失败";
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="IdStr"></param>
        /// <returns></returns>
        public ExecResult DeleteForm(QueryBase query)
        {
            ExecResult result = new ExecResult();
            int count = db.Deleteable<Form>().Where(x => x.ID == query.Int32Key).ExecuteCommand();
            if (count > 0)
            {
                result.code = 1;
                result.msg = "删除成功";
            }
            else
            {
                result.code = 0;
                result.msg = "删除失败";
            }
            return result;
        }
        #endregion

    }
}
