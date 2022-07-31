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
    public class UserController : ApiController
    {
        private readonly UserService UserService;
        private readonly ResultViewModel<UserViewModel> ResultViewModel;
        public UserController
            (
                UserService _UserService,
                ResultViewModel<UserViewModel> _ResultViewModel
            )
        {
            UserService = _UserService;
            ResultViewModel = _ResultViewModel;
        }

        [HttpGet]
        public ResultViewModel<IEnumerable<UserViewModel>> Get(string UserName = "" , string Email = "")
        {
            ResultViewModel<IEnumerable<UserViewModel>> result
                = new ResultViewModel<IEnumerable<UserViewModel>>();
            try
            {
                var Useres = UserService.GetAll(UserName, Email);
                result.Successed = true;
                result.Data = Useres;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;
        }

        [HttpGet]
        public ResultViewModel<UserViewModel> Get(int id)
        {
            ResultViewModel<UserViewModel> result
                = new ResultViewModel<UserViewModel>();
            try
            {
                var Users = UserService.GetByID(id);
                Users.Password = null;
                Users.UserName = null;
                result.Successed = true;
                result.Data = Users;
            }
            catch (Exception ex)
            {
                result.Successed = false;
                result.Message = "حدث خطأ ما";
            }
            return result;

        }



        [HttpPost]
        public ResultViewModel<UserViewModel> Login(UserLoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ResultViewModel.Successed = false;
                    ResultViewModel.Data = null;
                    ResultViewModel.Message = "يجب إدخال كلمة المرور و إسم المستخدم";
                }
                else
                {
                    UserViewModel userModel = UserService.Login(model);
                    if (userModel == null)
                    {
                        ResultViewModel.Successed = false;
                        ResultViewModel.Data = null;
                        ResultViewModel.Message = "خطأ فى إسم المرور أو كلمة السر";
                    }
                    else
                    {
                        ResultViewModel.Successed = true;
                        userModel.Password = null;
                        ResultViewModel.Data = userModel;
                        ResultViewModel.Message = "تم الدخول بنجاح";
                    }
                }
            }
            catch (Exception ex)
            {
                ResultViewModel.Successed = false;
                ResultViewModel.Data = null;
                ResultViewModel.Message = "جدث خطأ ما";
            }

            return ResultViewModel;
        }


    }
}
