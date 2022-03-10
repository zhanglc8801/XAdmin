using XAdmin.Common.Utils;
using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using System;
using System.Collections.Generic;
using System.Text;
using XAdmin.Model.CustomEntity;

namespace XAdmin.Service
{
    public class WorkFlowService: ServiceBase, IWorkFlowService
    {
        #region 表单类别
        /// <summary>
        /// 获取所有表单类别列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<FormType> GetFormTypeList(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<FormType> list = clientHelper.PostAuth<List<FormType>, QueryBase>(GetAPIUrl(APIConstants.GetFormTypeList), query);
            return list;
        }

        /// <summary>
        /// 获取表单类别实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public FormType GetFormTypeModel(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            FormType model = clientHelper.PostAuth<FormType, QueryBase>(GetAPIUrl(APIConstants.GetFormTypeModel), query);
            return model;
        }

        /// <summary>
        /// 保存表单类别信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveFormType(FormType model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, FormType>(GetAPIUrl(APIConstants.SaveFormType), model);
            return result;
        }

        /// <summary>
        /// 删除表单类别
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteFormType(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteFormType), query);
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
            HttpClientHelper clientHelper = new HttpClientHelper();
            List<Form> list = clientHelper.PostAuth<List<Form>, QueryBase>(GetAPIUrl(APIConstants.GetFormList), query);
            return list;
        }

        /// <summary>
        /// 获取表单实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Form GetFormModel(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            Form model = clientHelper.PostAuth<Form, QueryBase>(GetAPIUrl(APIConstants.GetFormModel), query);
            return model;
        }

        /// <summary>
        /// 保存表单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecResult SaveForm(Form model)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, Form>(GetAPIUrl(APIConstants.SaveForm), model);
            return result;
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ExecResult DeleteForm(QueryBase query)
        {
            HttpClientHelper clientHelper = new HttpClientHelper();
            ExecResult result = clientHelper.PostAuth<ExecResult, QueryBase>(GetAPIUrl(APIConstants.DeleteForm), query);
            return result;
        }
        #endregion
    }
}
