using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;

namespace EgyptianRecipes.Controllers
{
    public class BranchController : ApiController
    {
        private readonly BranchService BranchService;
        public BranchController(BranchService _BranchService)
        {
            BranchService = _BranchService;
        }

        [HttpGet]
        public ResultViewModel<PagingViewModel> Get(int pageIndex = 0, int pageSize = 3,
             int CustomerID = 0, string name = "", bool isDescinding = false)
        {

            ResultViewModel<PagingViewModel> result
                = new ResultViewModel<PagingViewModel>();
            try
            {
                var Branches = BranchService.Get(pageIndex, pageSize, CustomerID, name, isDescinding);
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

        [HttpGet]
        public ResultViewModel<IEnumerable<BranchViewModel>> SearchWithFilter(int pageIndex = 0, int pageSize = 3, string title = "")
        {
            ResultViewModel<IEnumerable<BranchViewModel>> result
                = new ResultViewModel<IEnumerable<BranchViewModel>>();
            try
            {
                var Branches = BranchService.SearchWithFilter(pageIndex, pageSize, title);
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }
        [HttpGet]
        public ResultViewModel<CustomerBranchEditViewModel> BookBranch(int branchID,int customerID)
        {
            ResultViewModel<CustomerBranchEditViewModel> result
                = new ResultViewModel<CustomerBranchEditViewModel>();
            try
            {
                var Branches = BranchService.BookBranch(branchID, customerID);
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }
        [HttpGet]
        public ResultViewModel<IEnumerable<BranchViewModel>> GetAll()
        {
            ResultViewModel<IEnumerable<BranchViewModel>> result
                = new ResultViewModel<IEnumerable<BranchViewModel>>();
            try
            {
                var Branches = BranchService.GetAll();
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }
        [HttpGet]
        public ResultViewModel<PagingViewModel> GetBookedBranches(int CustomerID, int pageIndex = 0, int pageSize = 3)
       {

            ResultViewModel<PagingViewModel> result
                = new ResultViewModel<PagingViewModel>();
            try
            {
                var Branches = BranchService.GetBookedBranches(CustomerID, pageIndex, pageSize);
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

        [HttpGet]
        public ResultViewModel<IEnumerable<BranchViewModel>> GetLatest(int index = 0, int CustomerID = 0)
        {
            ResultViewModel<IEnumerable<BranchViewModel>> result
                = new ResultViewModel<IEnumerable<BranchViewModel>>();
            try
            {
                var Branches = BranchService.GetLatest(index, CustomerID);
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;

        }

        [HttpGet]
        public ResultViewModel<BranchViewModel> Get(int id)
        {
            ResultViewModel<BranchViewModel> result
                = new ResultViewModel<BranchViewModel>();
            try
            {
                var Branches = BranchService.GetByID(id);
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;

        }
        [HttpGet]
        public ResultViewModel<BranchViewModel> GetLatestBranch(int customerID)
        {
            ResultViewModel<BranchViewModel> result
                = new ResultViewModel<BranchViewModel>();
            try
            {
                var Branches = BranchService.GetLatestBranch(customerID);
                result.Successed = true;
                result.Data = Branches;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;

        }

        [HttpPost]
        public ResultViewModel<BranchEditViewModel> Post(BranchEditViewModel model)
        {
            ResultViewModel<BranchEditViewModel> result
                = new ResultViewModel<BranchEditViewModel>();

            try
            {
                if (!ModelState.IsValid)
                {
                    result.Message = "In Valid Model State";
                }
                else
                {
                    BranchEditViewModel selectedUser
                        = BranchService.Add(model);
                    result.Successed = true;
                    result.Message = "تمت الإضافة بنجاح";
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
        public ResultViewModel<BranchEditViewModel> Put(BranchEditViewModel model)
        {
            ResultViewModel<BranchEditViewModel> result
                = new ResultViewModel<BranchEditViewModel>();

            try
            {
                if (!ModelState.IsValid)
                {
                    result.Message = "In Valid Model State";
                }
                else
                {
                    BranchEditViewModel selectedUser
                        = BranchService.Update(model);
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
        public ResultViewModel<BranchEditViewModel> Delete(int id)
        {
            ResultViewModel<BranchEditViewModel> result
                = new ResultViewModel<BranchEditViewModel>();

            try
            {

                BranchEditViewModel selectedUser
                    = BranchService.Remove(id);
                result.Successed = true;
                result.Data = selectedUser;

            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

        [HttpDelete]
        public ResultViewModel<CustomerBranchEditViewModel> CancelBookedBranch(int branchID, int customerID)
        {
            ResultViewModel<CustomerBranchEditViewModel> result
                = new ResultViewModel<CustomerBranchEditViewModel>();

            try
            {

                CustomerBranchEditViewModel selectedUser
                    = BranchService.CancelBookedBranch(branchID, customerID);
                result.Successed = true;
                result.Data = selectedUser;

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
