using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XAdmin.Model;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAdmin.IRepository;

namespace Web.Api.Controllers
{
    /// <summary>
    /// 资源文件操作
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResourcesAPIController : ControllerBase
    {
        private IResourcesRepository _repository;
        public ResourcesAPIController(IResourcesRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// 插入资源表数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ExecResult InsertResources(Resources model)
        {
            return _repository.InsertResources(model);
        }

        /// <summary>
        /// 获取所有资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResourcesModel> GetAllResourcesPageList(ResourcesQuery query)
        {
            return await _repository.GetAllResourcesPageList(query);
        }

        /// <summary>
        /// 根据条件获取资源分页列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResourcesModel> GetResourcesPageList(ResourcesQuery query)
        {
            return await _repository.GetResourcesPageList(query);
        }

        /// <summary>
        /// 根据ID字符串获取文件列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<List<Resources>> GetResourcesList(ResourcesQuery query)
        {
            return await _repository.GetResourcesList(query);
        }

        /// <summary>
        /// 根据文件ID获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Resources GetResourcesModel(ResourcesQuery query)
        {
            return _repository.GetResourcesModel(query);
        }

        /// <summary>
        /// 根据文件MD5获取资源实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Resources GetResourcesModelByMD5(ResourcesQuery query)
        {
            return _repository.GetResourcesModelByMD5(query);
        }
    }
}