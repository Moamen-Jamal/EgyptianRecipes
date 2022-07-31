using Services;
using System;
using System.Web.Http;
using ViewModels;

namespace WebApi.Controllers
{

    public class CustomerController : ApiController
    {
        private readonly CustomerService CustomerService;
        public CustomerController(CustomerService _CustomerService)
        {
            CustomerService = _CustomerService;
        }

        
        [HttpGet]
        public ResultViewModel<PagingViewModel> Get(int pageIndex = 0, int pageSize = 3)
        {

            ResultViewModel<PagingViewModel> result
                = new ResultViewModel<PagingViewModel>();
            try
            {
                var Customeres = CustomerService.Get(pageIndex, pageSize);
                result.Successed = true;
                result.Data = Customeres;
               
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

       
        [HttpGet]
        public ResultViewModel<CustomerViewModel> Get(int id)
        {
            ResultViewModel<CustomerViewModel> result
                = new ResultViewModel<CustomerViewModel>();
            try
            {
                var Customeres = CustomerService.GetByID(id);
                result.Successed = true;
                result.Data = Customeres;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;

        }

        [HttpPost]
        public ResultViewModel<CustomerEditViewModel> Post(CustomerEditViewModel model)
        {
            ResultViewModel<CustomerEditViewModel> result
                = new ResultViewModel<CustomerEditViewModel>();

            try
            {
                if (!ModelState.IsValid)
                {
                    result.Message = "In Valid Model State";
                }
                else
                {
                    CustomerEditViewModel selectedUser
                        = CustomerService.Add(model);
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

        [HttpPut]
    
        public ResultViewModel<CustomerEditViewModel> Put(CustomerEditViewModel model)
        {
            ResultViewModel<CustomerEditViewModel> result
                = new ResultViewModel<CustomerEditViewModel>(); 

            try
            {
                if (!ModelState.IsValid)
                {
                    result.Message = "In Valid Model State";
                }
                else
                {
                    CustomerEditViewModel selectedUser
                        = CustomerService.Update(model);
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
        
        [HttpDelete]
        public ResultViewModel<CustomerEditViewModel> Delete(int id)
        {
            ResultViewModel<CustomerEditViewModel> result
                = new ResultViewModel<CustomerEditViewModel>();

            try
            {
                CustomerService.Remove(id);
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
