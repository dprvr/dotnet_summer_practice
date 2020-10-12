using System;
using System.Web.Mvc;
using System.Web.Security;
using FilesStorage.BLL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Mappers;
using FilesStorage.Entities.ViewModels;

namespace FilesStorage.PL.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsersLogic _usersLogic;
        private readonly IPLMapper _mapper;

        private const string defaultController = "MyFiles";
        private const string defaultAction = "Index";

        public AuthController(IUsersLogic usersLogic, IPLMapper mapper)
        {
            _usersLogic = usersLogic;
            _mapper = mapper;
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(CreateUserView createUser)
        {
            if (ModelState.IsValid)
            {
                if (!_usersLogic.IsUserLoginExist(createUser.Login))
                {
                    _usersLogic.AddUserProfile(_mapper.Map<UserSignUpDto, CreateUserView>(createUser));
                    FormsAuthentication.SetAuthCookie(createUser.Login, false);
                    return RedirectToAction(defaultAction, defaultController);
                }
                else
                {
                    ModelState.AddModelError("", $"Login '{createUser.Login}' is already exist");
                }
            }
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLoginView userLog)
        {
            if (ModelState.IsValid)
            {
                var userLogDto = _mapper.Map<UserSignInDto, UserLoginView>(userLog);
                UserDto userProfile;
                var res = _usersLogic.IsUserLogDataValid(userLogDto, out userProfile);
                if (!res)
                {
                    ModelState.AddModelError("", "The entered login or password is incorrect");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userLog.Login, false);
                    return RedirectToAction(defaultAction, defaultController);
                }                
            }
            return View(userLog);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}