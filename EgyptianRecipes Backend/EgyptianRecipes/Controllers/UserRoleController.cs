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
    public class UserRoleController : ApiController
    {
        private readonly UserRoleService UserRoleService;
        public UserRoleController(UserRoleService _UserRoleService)
        {
            UserRoleService = _UserRoleService;
        }

        //GET api/UserRole
        [HttpGet]
        public IEnumerable<UserRoleViewModel> Get()
        {

            return UserRoleService.GetAll();
        }

        // GET api/UserRole/5
        [HttpGet]
        public UserRoleViewModel Get(int id)
        {
            return UserRoleService.GetByID(id);
        }

        // POST api/UserRole
        [HttpPost]
        public void Post(ViewModels.UserRoleEditViewModel model)
        {
            UserRoleService.Add(model);
        }

        // PUT api/UserRole/5
        [HttpPost]
        public void Put(UserRoleEditViewModel model)
        {
            UserRoleService.Update(model);
        }

        // DELETE api/UserRole/5
        [HttpGet]
        public void Delete(int id)
        {
            UserRoleService.Remove(id);
        }
    }
}
