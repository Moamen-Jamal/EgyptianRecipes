using Services;
using System;
using System.Web.Http;
using ViewModels;
using WebApi.Filter;

namespace WebApi.Controllers
{
    [AUTHORIZE(Roles = "Admin")]
    public class AdminController : ApiController
    {
        private readonly AdminService AdminService;
        private readonly DashBoardService DashBoardService;
        public AdminController(AdminService _AdminService, DashBoardService _DashBoardService)
        {
            AdminService = _AdminService;
            DashBoardService = _DashBoardService;
        }

        //GET api/Admin
        [HttpGet]
        public ResultViewModel<PagingViewModel> Get(int pageIndex, int pageSize )
        {

            ResultViewModel<PagingViewModel> result
                = new ResultViewModel<PagingViewModel>();
            try
            {
                var Admins = AdminService.Get(pageIndex, pageSize);
                result.Successed = true;
                result.Data = Admins;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

        // GET api/Admin/5
        [HttpGet]
        public ResultViewModel<AdminViewModel> Get(int id)
        {
            ResultViewModel<AdminViewModel> result
                = new ResultViewModel<AdminViewModel>();
            try
            {
                var Admins = AdminService.GetByID(id);
                result.Successed = true;
                result.Data = Admins;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;

        }

        // POST api/Admin
        [HttpPost]
        public ResultViewModel<AdminEditViewModel> Post(AdminEditViewModel model)
        {
            ResultViewModel<AdminEditViewModel> result
                = new ResultViewModel<AdminEditViewModel>();

            try
            {
                if (!ModelState.IsValid)
                {
                    result.Message = "In Valid Model State";
                }
                else
                {
                    AdminEditViewModel selectedUser
                        = AdminService.Add(model);
                    result.Successed = true;
                    result.Data = selectedUser;
                }
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

        // PUT api/Admin/5
        [HttpPut]
        public ResultViewModel<AdminEditViewModel> Put(AdminEditViewModel model)
        {
            ResultViewModel<AdminEditViewModel> result
                = new ResultViewModel<AdminEditViewModel>();

            try
            {
                if (!ModelState.IsValid)
                {
                    result.Message = "In Valid Model State";
                }
                else
                {

                    AdminEditViewModel selectedUser
                        = AdminService.Update(model);
                    result.Successed = true;
                    result.Data = selectedUser;
                }
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

        // DELETE api/Admin/5
        [HttpDelete]
        public ResultViewModel<AdminEditViewModel> Delete(int id)
        {
            ResultViewModel<AdminEditViewModel> result
                = new ResultViewModel<AdminEditViewModel>();

            try
            {

                AdminService.Remove(id);
                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }


        [HttpGet]
        public ResultViewModel<DashboardViewModel> GetDashboardDetails()
        {
            ResultViewModel<DashboardViewModel> result
                = new ResultViewModel<DashboardViewModel>();

            try
            {

                result.Data = DashBoardService.GetStatistics();
                result.Successed = true;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }
    }
}
