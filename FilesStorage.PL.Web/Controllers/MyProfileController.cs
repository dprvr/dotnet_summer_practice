using System.Web.Mvc;
using FilesStorage.BLL.Interfaces;
using FilesStorage.Entities.DTOs;
using FilesStorage.Entities.Mappers;
using FilesStorage.Entities.ViewModels;

namespace FilesStorage.PL.Web.Controllers
{
    [Authorize]
    public class MyProfileController : Controller
    {
        private readonly IUsersLogic _usersLogic;
        private readonly IPLMapper _mapper;

        public MyProfileController(IUsersLogic usersLogic, IPLMapper mapper)
        {
            _usersLogic = usersLogic;
            _mapper = mapper;
        }

        private UserDto GetUser()
        {
            return _usersLogic.FindUserByLogin(User.Identity.Name);
        }

        [HttpGet]
        public ActionResult Index()
        {
            UserProfileView userProfile = _mapper.Map<UserProfileView, UserDto>(GetUser());
            if(userProfile == null)
            {
                return new HttpNotFoundResult($"Can't found user with login '{User.Identity.Name}'.");
            }
            return View(userProfile);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var dto = GetUser();
            var userProfileView = _mapper.Map<UpdateUserProfileView, UserDto>(dto);
            return View(userProfileView);
        }

        [HttpPost]
        public ActionResult Edit(UpdateUserProfileView userProfileView)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<UserSignUpDto, UpdateUserProfileView>(userProfileView);
                _usersLogic.UpdateUserProfile(dto);
                return RedirectToAction("Index", "MyProfile");
            }
            return View(userProfileView);
        }
        
    }
}