using XAdmin.Model;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAdmin.Model.CustomEntity;

namespace XAdmin.IRepository
{
    public interface IWorkFlowRepository
    {
        #region 表单类别
        /// <summary>
        /// 获取所有表单类别列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<FormType> GetFormTypeList(QueryBase query);

        /// <summary>
        /// 获取表单类别实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        FormType GetFormTypeModel(QueryBase query);

        /// <summary>
        /// 保存表单类别信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveFormType(FormType model);

        /// <summary>
        /// 删除表单类别
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteFormType(QueryBase query);
        #endregion

        #region 表单设计
        /// <summary>
        /// 获取所有表单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<Form> GetFormList(QueryBase query);

        /// <summary>
        /// 获取表单实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Form GetFormModel(QueryBase query);

        /// <summary>
        /// 保存表单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ExecResult SaveForm(Form model);

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        ExecResult DeleteForm(QueryBase query);
        #endregion
    }
}
