using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProcessingIK.Controllers
{
    public class EmployeeController : Controller
    {
        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;
        public SignInManager<AppUser> _signInManager;
        private readonly IUserVacationService _userVacationService;
        private readonly IUserCompanyService _userCompanyService;

        public EmployeeController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserCompanyService userCompanyService,IUserVacationService userVacationService)
        {
            _userVacationService = userVacationService;
            _userCompanyService = userCompanyService;
            _roleManager = roleManager;

            _signInManager = signInManager;
            _userManager = userManager;


        }
        public IActionResult Index()
        {
            return View();
        }

    
 
        [HttpGet]
        public IActionResult UserEdit()
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserEditDto user = appUser.Adapt<UserEditDto>();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditDto userEditDto)
        {
            ModelState.Remove("Password");
            //ModelState.Remove(userEditDto.Password);
            if (ModelState.IsValid)
            {
                AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                appUser.UserName = userEditDto.UserName;
                appUser.FirstName = userEditDto.FirstName;
                appUser.LastName = userEditDto.LastName;
                appUser.PhoneNumber = userEditDto.PhoneNumber;
                appUser.BirthPlace = userEditDto.BirthPlace;
                appUser.BirthDate = userEditDto.BirthDate;
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(appUser);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(appUser, true);
                    ViewBag.succeeded = "true";
                }
                else
                {
                    AddModelError(result);
                }
            }
            return View(userEditDto);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
                if (appUser != null)
                {
                    bool exist = _userManager.CheckPasswordAsync(appUser, changePasswordDto.PasswordOld).Result;
                    if (exist == true)
                    {
                        if (changePasswordDto.PasswordNew == changePasswordDto.PasswordConfirm)
                        {
                            IdentityResult result = _userManager.ChangePasswordAsync(appUser, changePasswordDto.PasswordOld, changePasswordDto.PasswordNew).Result;
                            if (result.Succeeded)
                            {
                                _userManager.UpdateSecurityStampAsync(appUser);
                                _signInManager.SignOutAsync();
                                _signInManager.PasswordSignInAsync(appUser, changePasswordDto.PasswordNew, true, false);
                                ViewBag.status = "Successful";
                            }
                            else
                            {
                                AddModelError(result);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Eski şifreniz yanlıştır");
                    }
                }
            }
            return View(changePasswordDto);
        }

        public IActionResult RequestVocation()
        {


            return View();
        }


        [HttpPost]
        public async   Task<IActionResult> RequestVocation(UserVacationDto userVacationDto1)
        {
            AppUser user = new AppUser();
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ViewBag.employeeıd = usera.Id;
            var companyFind = _userCompanyService.GetByUserId(usera.Id);



            var companyList = _userCompanyService.GetList();
            var result = (from x in companyList.ToList()
                          select new
                          {
                              x.UserId,

                              x.CompanyId

                          }).Where(x => x.CompanyId == companyFind.CompanyId).ToList();


            foreach (var role in result)
            {
                ViewBag.userıd = role.UserId;
                AppUser app = await _userManager.FindByIdAsync(ViewBag.userıd);
                IList<string> rolesa = await _userManager.GetRolesAsync(app);
                foreach (var item in rolesa)
                {
                    if (item.Contains("Manager"))
                    {
                        ViewBag.managerId = app.Id;
                    }
                }



            }

            UserVacationDto  userVacationDto = new UserVacationDto();
            userVacationDto.UserId = ViewBag.employeeıd;
            userVacationDto.StartDate = userVacationDto1.StartDate;
            userVacationDto.EndDate = userVacationDto1.EndDate;
            userVacationDto.HolidayId = userVacationDto1.HolidayId;
            userVacationDto.ManagerApprovedId = ViewBag.managerId;
            userVacationDto.IsConfirmed = false;
           _userVacationService.Add(userVacationDto);




            return View(userVacationDto1);
        }
  






        [HttpGet]
        public IActionResult PassiveUser()
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ViewBag.userName = appUser.UserName;
            var result = (from x in _userManager.Users
                          select new
                          {
                              IsPassive = x.IsConfirmed
                          }).ToList();
            UserPassivizationDto userPassivizationDto = new UserPassivizationDto();
            userPassivizationDto.UserName = appUser.UserName;
            userPassivizationDto.IsConfirmed = appUser.IsConfirmed;

            if (userPassivizationDto.IsConfirmed == true)
            {
                userPassivizationDto.Exist = true;
            }
            else
            {
                userPassivizationDto.Exist = false;
            }
            return View(userPassivizationDto);
        }

        [HttpPost]
        public async Task<IActionResult> PassiveUser(UserPassivizationDto userPassivizationDto)
        {
            AppUser appUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (userPassivizationDto.Exist)
            {
                appUser.IsConfirmed = true;
            }
            else
            {
                appUser.IsConfirmed = false;
            }
            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(appUser);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(appUser, true);
                ViewBag.succeeded = "true";
                if (appUser.IsConfirmed == false)
                {
                    return RedirectToAction("LogIn", "home");
                }
            }
            else
            {
                AddModelError(result);
            }
            return RedirectToAction("Index", "Home");
        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }



    }
}
