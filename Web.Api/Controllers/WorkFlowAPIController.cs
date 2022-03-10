using XAdmin.Model;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.IRepository;
using XAdmin.Model.CustomEntity;

namespace Web.Api.Controllers
{
    /// <summary>
    /// 流程操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WorkFlowAPIController : ControllerBase
    {
        private IWorkFlowRepository _repository;
        public WorkFlowAPIController(IWorkFlowRepository repository)
        {
            this._repository = repository;
        }

        #region 表单类别
        /// <summary>
        /// 获取所有表单类别列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<FormType> GetFormTypeList(QueryBase query)
        {
            return _repository.GetFormTypeList(query);
        }

        /// <summary>
        /// 获取表单类别实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public FormType GetFormTypeModel(QueryBase query)
        {
            return _repository.GetFormTypeModel(query);
        }

        /// <summary>
        /// 保存表单类别信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveFormType(FormType model)
        {
            return _repository.SaveFormType(model);
        }

        /// <summary>
        /// 删除表单类别
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteFormType(QueryBase query)
        {
            return _repository.DeleteFormType(query);
        }
        #endregion

        #region 表单设计
        /// <summary>
        /// 获取所有表单列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<Form> GetFormList(QueryBase query)
        {
            return _repository.GetFormList(query);
        }

        /// <summary>
        /// 获取表单实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Form GetFormModel(QueryBase query)
        {
            return _repository.GetFormModel(query);
        }

        /// <summary>
        /// 保存表单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult SaveForm(Form model)
        {
            return _repository.SaveForm(model);
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult DeleteForm(QueryBase query)
        {
            return _repository.DeleteForm(query);
        }
        #endregion

    }
}
