using XAdmin.IService;
using XAdmin.Model;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XAdmin.Model.CustomEntity;

namespace Web.Admin.Controllers
{
    public class WorkFlowController : BaseController
    {
        private readonly IWorkFlowService _workFlowService;
        public WorkFlowController(IWorkFlowService workFlowService)
        {
            this._workFlowService = workFlowService;
        }

        public IActionResult FormType()
        {
            return View();
        }

        /// <summary>
        /// 获取所有表单类别列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetFormTypeList(QueryBase query)
        {
            query.SiteID = CurUser.UserModel.SiteID;
            List<FormType> list = _workFlowService.GetFormTypeList(query);
            if (list.Count>0)
                return Json(new { code = 0, data=list, msg = "" });
            else
                return Json(new { code = 1, msg = "没有获取到数据" });
        }

        /// <summary>
        /// 表单类别 新增/编辑视图页
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult FormTypeEdit(int ID = 0)
        {
            FormType model = new FormType();
            if (ID != 0)
            {
                QueryBase query = new QueryBase();
                query.Int32Key = ID;
                model = _workFlowService.GetFormTypeModel(query);
                return View(model);
            }
            else
            {
                model.ID = 0;
                model.Note = "";
                return View(model);
            }
        }

        /// <summary>
        /// 保存表单类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveFormType(FormType model)
        {
            ExecResult result = new ExecResult();
            model.SiteID = CurUser.UserModel.SiteID;
            result = _workFlowService.SaveFormType(model);
            return Json(new { code = result.code, msg = result.msg });
        }

        /// <summary>
        /// 删除表单类别
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteFormType(QueryBase query)
        {
            ExecResult result = new ExecResult();
            query.SiteID = CurUser.UserModel.SiteID;
            result = _workFlowService.DeleteFormType(query);
            return Json(new { code = result.code, msg = result.msg });
        }

    }
}
