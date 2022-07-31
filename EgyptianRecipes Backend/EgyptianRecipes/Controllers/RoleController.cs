using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;
using WebApi.Filter;

namespace WebApi.Controllers
{
    public class RoleController : ApiController
    {
        private readonly RoleService RoleService;
        public RoleController(RoleService _RoleService)
        {
            RoleService = _RoleService;
        }

        //GET api/Role
        [HttpGet]
        public IEnumerable<RoleViewModel> Get()
        {

            return RoleService.GetAll();
        }

        // GET api/Role/5
        [HttpGet]
        public RoleViewModel Get(int id)
        {
            return RoleService.GetByID(id);
        }

        // POST api/Role
        [HttpPost]
        public void Post(ViewModels.RoleEditViewModel model)
        {
            RoleService.Add(model);
        }

        // PUT api/Role/5
        [HttpPut]
        public void Put(RoleEditViewModel model)
        {
            RoleService.Update(model);
        }

        // DELETE api/Role/5
        [HttpDelete]
        public void Delete(int id)
        {
            RoleService.Remove(id);
        }
    }
}
