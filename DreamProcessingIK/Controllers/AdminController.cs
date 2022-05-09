using Business.Abstract;
using Business.Concrete;
using Core.Helper;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using DreamProcessingIK.Managers;
using Entities.Concrete;
using Entities.Dtos;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamProcessingIK.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserVacationService _userVacationService;
        private readonly IVacationService _vacationService;
        private readonly ISectorService _sectorService;
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        private readonly ICompanySectorService _companySectorService;
        private readonly IUserCompanyService _companyUserService;

        public UserManager<AppUser> _userManager;
        public SignInManager<AppUser> _signInManager;
        public RoleManager<AppRole> _roleManager;

        public AdminController(IUserService userSevice, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ICompanyService companyService, ICompanySectorService companySectorService, ISectorService sectorService, IVacationService vacationService, IUserVacationService userVacationService, IWebHostEnvironment webHostEnvironment, IUserCompanyService userCompanyService)
        {
            _companyUserService = userCompanyService;
            _webHostEnvironment = webHostEnvironment;
            _userVacationService = userVacationService;
            _vacationService = vacationService;
            _sectorService = sectorService;
            _companyService = companyService;
            _userManager = userManager;
            _userService = userSevice;
            _roleManager = roleManager;
            _companySectorService = companySectorService;
        }


        #region About User

        #endregion
        public IActionResult Users()
        {
            return View(_userManager.Users.ToList());
        }
        public IActionResult ManagerList()
        {
            List<AppUser> result = _userManager.Users.ToList();
            List<AppUser> users = new List<AppUser>();
            foreach (var item in result)
            {
                List<string> userRoles = _userManager.GetRolesAsync(item).Result as List<string>;
                if (userRoles.Contains("Manager") && item.EmailConfirmed == true && item.IsConfirmed == true)
                {
                    users.Add(item);
                }
            }
            return View(users);
        }
        public IActionResult PersonelList()
        {
            List<AppUser> result = _userManager.Users.ToList();
            List<AppUser> employee = new List<AppUser>();
            foreach (var item in result)
            {
                List<string> userRoles = _userManager.GetRolesAsync(item).Result as List<string>;
                if (userRoles.Contains("Personel") && item.EmailConfirmed == true && item.IsConfirmed == true)
                {
                    employee.Add(item);
                }
            }
            return View(employee);
        }
        public IActionResult UsersConfirm()
        {
            List<AppUser> users = _userManager.Users.ToList();
            List<UserNotConfirmedDto> usersNotConfirmed = new List<UserNotConfirmedDto>();
            foreach (var item in users)
            {
                List<string> userRoles = _userManager.GetRolesAsync(item).Result as List<string>;
                if (userRoles.Contains("Manager") && item.EmailConfirmed == true && item.IsConfirmed == false)
                {
                    UserNotConfirmedDto userNotConfirmed = new UserNotConfirmedDto()
                    {
                        UserId = item.Id,
                        UserName = item.UserName,
                        IsConfirmed = item.IsConfirmed,
                        Exist = item.IsConfirmed
                    };
                    usersNotConfirmed.Add(userNotConfirmed);
                }
            }
            return View(usersNotConfirmed);
        }

        public IActionResult UsersNotConfirmed(string id)
        {
            TempData["userId"] = id;
            AppUser user = _userManager.FindByIdAsync(id).Result;

            List<UserNotConfirmedDto> usersNotConfirmed = new List<UserNotConfirmedDto>();
            IQueryable<AppRole> roles = _roleManager.Roles;

            List<string> userRoles = _userManager.GetRolesAsync(user).Result as List<string>;
            if (userRoles.Contains("Manager") && user.EmailConfirmed == true && user.IsConfirmed == false)
            {
                UserNotConfirmedDto r = new UserNotConfirmedDto();
                r.UserId = user.Id;
                r.IsConfirmed = user.IsConfirmed;
                r.Exist = user.IsConfirmed;


                usersNotConfirmed.Add(r);
            }


            return View(usersNotConfirmed);
        }

        [HttpPost]
        public async Task<IActionResult> UsersNotConfirmed(List<UserNotConfirmedDto> userNotConfirmedDto)
        {

            AppUser appUser = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;
            foreach (var item in userNotConfirmedDto)
            {
                if (item.Exist)
                {
                    appUser.IsConfirmed = true;

                }
                else
                {
                    appUser.IsConfirmed = false;
                }
            }

            IdentityResult result = await _userManager.UpdateAsync(appUser);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(appUser);

                ManagerConfirmHelper.ManagerConfirmEmail(appUser.Email);
                ViewBag.succeeded = "true";
            }

            return RedirectToAction("Users", "Admin");
        }

        [HttpGet]
        public IActionResult PassiveUser(string Id)
        {
            AppUser appUser = _userManager.FindByIdAsync(Id).Result;
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
            TempData["userId"] = Id;
            return View(userPassivizationDto);
        }

        [HttpPost]
        public async Task<IActionResult> PassiveUser(UserPassivizationDto userPassivizationDto)
        {
            AppUser appUser = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;
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
                ViewBag.succeeded = "true";
                if (appUser.IsConfirmed == false)
                {
                    return RedirectToAction("Users", "Admin");
                }
            }
            else
            {
                AddModelError(result);
            }
            return RedirectToAction("Users", "Admin");
        }

        #region AboutRole
        public IActionResult RoleCreate()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RoleCreate(RoleCreateDto roleCreateDto)
        {
            AppRole role = new AppRole();
            role.Name = roleCreateDto.Name;
            IdentityResult result = _roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {

                return RedirectToAction("Roles");
            }
            else
            {
                AddModelError(result);
            }

            return View(roleCreateDto);
        }


        public IActionResult Roles()
        {
            return View(_roleManager.Roles.ToList());
        }


        public IActionResult RoleAssign(string id)
        {
            TempData["userId"] = id;
            AppUser user = _userManager.FindByIdAsync(id).Result;
            ViewBag.userName = user.UserName;
            IQueryable<AppRole> roles = _roleManager.Roles;
            List<string> userRoles = _userManager.GetRolesAsync(user).Result as List<string>;
            List<RoleAssignDto> roleAssignDtos = new List<RoleAssignDto>();
            foreach (AppRole role in roles)
            {
                RoleAssignDto r = new RoleAssignDto();
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userRoles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }
                roleAssignDtos.Add(r);

            }
            return View(roleAssignDtos);
        }


        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignDto> roleAssignDtos)
        {
            AppUser user = await _userManager.FindByIdAsync(TempData["userId"].ToString());
            foreach (var item in roleAssignDtos)
            {
                if (item.Exist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("Users");
        }
        #endregion

        #region AboutCompany
        [HttpGet]
        public IActionResult CreateCompany()
        {
            CreateCompanyForAdminDto createCompanyForAdminDto = new CreateCompanyForAdminDto();
            createCompanyForAdminDto.Sectors = _sectorService.GetList();
            return View(createCompanyForAdminDto);
        }

        [HttpPost]
        public IActionResult CreateCompany(CreateCompanyForAdminDto createCompanyForAdminDto)
        {
            Company company = new Company();
            company.CompanyName = createCompanyForAdminDto.CompanyName;
            company.CompanyDetail = createCompanyForAdminDto.CompanyDescription;
            company.StartDate = createCompanyForAdminDto.StartDate;
            company.EndDate = createCompanyForAdminDto.EndDate;
            company.Logo = createCompanyForAdminDto.Logo.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment);
            _companyService.Add(company);
            //foreach (var item in createCompanyForAdminDto.Sectors)
            //{
            //    CompanySectorDto companySectorDto = new CompanySectorDto();
            //    if (item.Exist)
            //    {
            //        companySectorDto.SectorId = item.Id;
            //        companySectorDto.CompanyId = company.Id;
            //        _companySectorService.Add(companySectorDto);
            //    }
            //}
            return View(createCompanyForAdminDto);
        }

        [HttpGet]
        public IActionResult EditCompany(int id)
        {
            Company company = _companyService.GetById(id);
            TempData["companyId"] = id;
            CreateCompanyForAdminDto createCompanyForAdminDto = new CreateCompanyForAdminDto();
            createCompanyForAdminDto.CompanyName = company.CompanyName;
            createCompanyForAdminDto.CompanyDescription = company.CompanyDetail;
            createCompanyForAdminDto.StartDate = (DateTime)company.StartDate;
            createCompanyForAdminDto.EndDate = (DateTime)company.EndDate;
            return View(createCompanyForAdminDto);
        }

        [HttpPost]
        public IActionResult EditCompany(CreateCompanyForAdminDto createCompanyForAdminDto)
        {
            Company company = _companyService.GetById((int)TempData["companyId"]);
            company.CompanyName = createCompanyForAdminDto.CompanyName;
            company.CompanyDetail = createCompanyForAdminDto.CompanyDescription;
            company.StartDate = createCompanyForAdminDto.StartDate;
            company.EndDate = createCompanyForAdminDto.EndDate;
            company.Logo = createCompanyForAdminDto.Logo.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment);
            _companyService.Update(company);
            //foreach (var item in createCompanyForAdminDto.Sectors)
            //{
            //    CompanySectorDto companySectorDto = new CompanySectorDto();
            //    if (item.Exist)
            //    {
            //        companySectorDto.SectorId = item.Id;
            //        companySectorDto.CompanyId = company.Id;
            //        _companySectorService.Update(companySectorDto);
            //    }
            //}
            return View(createCompanyForAdminDto);
        }

        public IActionResult GetCompanies()
        {
            return View(_companyService.GetList());
        }

        #endregion

        #region AboutVacations
        public IActionResult CreateVacation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateVacation(Vacation vacation)
        {
            _vacationService.Add(vacation);
            return RedirectToAction("Vacations", "Admin");
        }
        public IActionResult EditVacation(int id)
        {
            Vacation vacation = _vacationService.GetById(id);
            return View(vacation);
        }
        [HttpPost]
        public IActionResult EditVacation(Vacation vacation)
        {
            _vacationService.Update(vacation);
            return RedirectToAction("Vacations", "Admin");
        }

        public IActionResult DeleteVacation(int id)
        {
            _vacationService.Delete(_vacationService.GetById(id));
            return RedirectToAction("Vacations", "Admin");
        }
        public IActionResult Vacations()
        {
            return View(_vacationService.GetList());
        }



        public IActionResult CreateUserVacation()
        {
            CreateVacationUserDto createVacationUserDto = new CreateVacationUserDto();
            createVacationUserDto.Users = _userManager.Users.ToList();
            createVacationUserDto.Vacations = _vacationService.GetList();
            return View(createVacationUserDto);
        }
        [HttpPost]
        public IActionResult CreateUserVacation(CreateVacationUserDto createVacationUserDto)
        {
            UserVacationDto userVacationDto = new UserVacationDto();
            userVacationDto.UserId = createVacationUserDto.UserId;
            userVacationDto.HolidayId = createVacationUserDto.VacationId;
            userVacationDto.StartDate = createVacationUserDto.StartDate;
            userVacationDto.EndDate = createVacationUserDto.EndDate;
            _userVacationService.Add(userVacationDto);
            return RedirectToAction("Calendar", "Calendar");
        }

        public IActionResult UserVacations()
        {
            var vacations = _userVacationService.GetList();
            return new JsonResult(vacations);
        }
        #endregion


        public IActionResult Index()
        {
            return View(_companySectorService.GetList());
        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {

                ModelState.AddModelError("", item.Description);

            }

        }

        public IActionResult _AdminDashboard()
        {

            return View();
        }

        public IActionResult CompanySectorList()  //kontrol edilmesi gerekiyor
        {
            CompanySectorListDto dto = new CompanySectorListDto();
            dto.Companies = _companyService.GetList();
            dto.Sectors = _sectorService.GetList();

            return View(dto);
        }
        public IActionResult Analytics()
        {
            List<int> vs = new List<int>
            {
                0, 1, 2, 3, 4, 5, 6
            };
            return new JsonResult(vs);
        }
        

        #region Grafik
        public IActionResult Chart()
        {
            return View();
        }
        public IActionResult Visule()
        {
            return Json(VisualizeResult());
        }
        public List<ChartAllDto> VisualizeResult()
        {
            List<ChartAllDto> total = new List<ChartAllDto>();

            List<AppUser> result = _userManager.Users.ToList();
            List<AppUser> users = new List<AppUser>();
            List<AppUser> employee = new List<AppUser>();
            ChartAllDto allDto = new ChartAllDto();
            ChartAllDto allDto1 = new ChartAllDto();
            ChartAllDto allDto2 = new ChartAllDto();
            foreach (var item in result)
            {
                List<string> userRoles = _userManager.GetRolesAsync(item).Result as List<string>;
                if (userRoles.Contains("Manager") && item.EmailConfirmed == true && item.IsConfirmed == true)
                {
                    users.Add(item);
                }
            }
            foreach (var item in result)
            {
                List<string> userRoles = _userManager.GetRolesAsync(item).Result as List<string>;
                if (userRoles.Contains("Personel") && item.EmailConfirmed == true && item.IsConfirmed == true)
                {
                    employee.Add(item);
                }
            }

            allDto.count = users.Count();
            allDto.name = "Yönetici Sayisi";
            allDto1.count = _companyService.GetList().Count();
            allDto1.name = "Şirket Sayisi";
            allDto2.count = employee.Count;
            allDto2.name = "Personel Sayisi";

            total.Add(allDto);
            total.Add(allDto1);
            total.Add(allDto2);
            return total;
        }

        #endregion
    }
}
