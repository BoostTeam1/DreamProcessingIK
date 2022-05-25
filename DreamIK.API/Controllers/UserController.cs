using DreamProcessingIK.Controllers;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DreamIK.API.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;
        public RoleManager<AppRole> _roleManager;

        HomeController _homeController;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, HomeController homeController)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _homeController = homeController;   
        }
        [Route(template:"api/alluser")]
        [HttpGet]
        public IActionResult Index()
        {
            UserForLoginDto userForLoginDto = new UserForLoginDto() { EMail = "coymax0@gmail.com", Password = "45526201Co"};
            _homeController.LogIn(userForLoginDto);
            var result = _userManager.Users.ToList();
            return Ok(result);
        }


    }
}
