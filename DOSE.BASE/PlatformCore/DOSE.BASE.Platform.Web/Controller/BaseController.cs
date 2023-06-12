using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.BL.Model;

namespace DOSE.BASE.PlatformCore.Web.Controller
{
    public class BaseController : BaseCoreController
    {
        public BaseController(CoreServiceCollection serviceCollection): base(serviceCollection)
        {

        }

        [HttpPost]
        [Route("save-async")]
        public async Task<ServiceRespone> SaveOne([FromBody] object obj)
        {
            try
            {
                var model = (BaseModel)JsonConvert.DeserializeObject(obj.ToString(), CurrentModelType);
                _serviceRespone.Data = await _BL.AddOrUpdate(model);
                return _serviceRespone.OnSuccess();
            }
            catch(Exception ex)
            {
                return _serviceRespone.OnError(ex.InnerException.ToString());
            }
        }
        // GET: api/<TestController>
        [HttpPost]
        [Route("get-all")]
        public async Task<ServiceRespone> GetAllAsync(PagingRequest pagingRequest)
        {
            //var baseModel = (BaseModel)Activator.CreateInstance(CurrentModelType);
            //var validateResult = this.BL.CheckPermission(baseModel, this.BL.GetPermissionView(baseModel));
            try
            {
                _serviceRespone.Data = await _BL.GetAllAsync(CurrentModelType, pagingRequest.Filter, pagingRequest.Sort);
                return _serviceRespone.OnSuccess();

            }
            catch(Exception ex)
            {
                return _serviceRespone.OnError(ex.Message);
            }
        }
        // GET: api/<TestController>
        [HttpPost]
        [Route("paging")]
        public async Task<ServiceRespone> GetPagingAsync([FromBody]PagingRequest pagingRequest)
        {
            try
            {
                _serviceRespone.Data = await _BL.GetPagingAsync(CurrentModelType, pagingRequest);
                return _serviceRespone.OnSuccess();

            }
            catch(Exception ex)
            {
                return _serviceRespone.OnError(ex.InnerException.ToString());
            }
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public async Task<ServiceRespone> GetByIDAsync(int id)
        {
            try
            {
                _serviceRespone.Data = await _BL.GetByIDAsync(CurrentModelType, id.ToString());
                return _serviceRespone.OnSuccess();

            }
            catch (Exception ex)
            {
                return _serviceRespone.OnError(ex.InnerException.ToString());
            }
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
