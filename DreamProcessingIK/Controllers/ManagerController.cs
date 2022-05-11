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
    public class ManagerController : Controller
    {

        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;
        public SignInManager<AppUser> _signInManager;
        private readonly IUserCompanyService _userCompanyService;
        private readonly IUserVacationService _userVacationService;
        private readonly IUserDebitService _userDebitService;
        private readonly IDebitService _debitService;
        private readonly ICategoryService _categoryService;
        public ManagerController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserCompanyService userCompanyService, IUserVacationService userVacationService, IUserDebitService userDebitService, IDebitService debitService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _debitService = debitService;
            _userDebitService = userDebitService;
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
        [HttpGet]
        public IActionResult AddEmployee()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeAddDto employeeAddDto, UserCompanyDto userCompanyDto)
        {
            AppUser user = new AppUser();
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ViewBag.userID = usera.Id;
            user.FirstName = employeeAddDto.FirstName;
            user.UserName = employeeAddDto.UserName;
            user.Email = employeeAddDto.Email;
            user.LastName = employeeAddDto.LastName;
            user.IsConfirmed = employeeAddDto.IsConfirmed;
            user.EmailConfirmed = employeeAddDto.EmailConfirmed;
            IdentityResult result = await _userManager.CreateAsync(user, employeeAddDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Personel");
                var managerCompany = _userCompanyService.GetByUserId(ViewBag.userID);
                employeeAddDto.UserId = user.Id;
                userCompanyDto.UserId = employeeAddDto.UserId;
                userCompanyDto.CompanyId = managerCompany.CompanyId;
                _userCompanyService.Add(userCompanyDto);

                ViewBag.succeeded = "true";
            }
            else
            {
                AddModelError(result);
            }
            return View(employeeAddDto);
        }
        [HttpGet]
        public IActionResult ApprovedVacation()
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
            var userVoca = _userVacationService.GetList();
            var findUserVoca = (from x in userVoca.ToList()
                                select new
                                {
                                    x.ManagerApprovedId,
                                    x.UserId,
                                    x.IsConfirmed,

                                }).ToList();

            foreach (var item in findUserVoca)
            {
                if (item.ManagerApprovedId == usera.Id)
                {
                    break;
                }

            }
            return View();
        }

        [HttpPost]
        public IActionResult ApprovedVocation()
        {



            return View();
        }

        [HttpGet]
        public IActionResult GivenDebit()
        {
            AppUser user = new AppUser();
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var companyFind = _userCompanyService.GetByUserId(usera.Id);
            var companyList = _userCompanyService.GetList();
            var result = (from x in companyList.ToList()
                          join u in _userManager.Users.ToList() on x.UserId equals u.Id
                          join ud in _userDebitService.GetList().ToList() on u.Id equals ud.UserId
                          join d in _debitService.GetList().ToList() on ud.DebitId equals d.Id
                          join c in _categoryService.GetList().ToList() on d.CategoryId equals c.Id
                          select new
                          {
                              x.UserId,
                              u.FirstName,
                              u.LastName,
                              ud.StartDate,
                              ud.EndDate,
                              c.CategoryName,
                              d.ProductName,
                              d.ProductDetail,
                              x.CompanyId

                          }).Where(x => x.CompanyId == companyFind.CompanyId).ToList();

            List<RequestDebitVmDto> debit = new List<RequestDebitVmDto>();
            foreach (var item in result)
            {
                debit.Add(new RequestDebitVmDto()
                {
                    UserId= item.UserId,
                    FirstName=item.FirstName,
                    LastName=item.LastName,
                    StartDate=item.StartDate,
                    EndDate=item.EndDate,
                    CategoryName=item.CategoryName,
                    ProductName=item.ProductName,
                    ProductDetail=item.ProductDetail,

                });


            }
            return View(debit);
        }

   [HttpGet]
        public IActionResult CompanyEmployee()
        {
            AppUser user = new AppUser();
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;
       
            var companyFind = _userCompanyService.GetByUserId(usera.Id);



            var companyList = _userCompanyService.GetList();
            var result = (from x in companyList.ToList() 
                          join u in _userManager.Users.ToList() on x.UserId equals u.Id
                          select new
                          {
                              x.UserId,
                              u.FirstName,
                              u.LastName,

                              x.CompanyId

                          }).Where(x => x.CompanyId == companyFind.CompanyId).ToList();
            List<EmployeeListCompanyDto> employeeLists = new List<EmployeeListCompanyDto>();
       
            foreach (var item in result)
            {
                if (usera.Id !=item.UserId)
                {
                employeeLists.Add(new EmployeeListCompanyDto()
                {
                    UserId=item.UserId,
                    FirstName=item.FirstName,
                    LastName=item.LastName
                });
                }
            }


            return View(employeeLists);
        }


        [HttpGet]
        public IActionResult AddDebit(string id)
        {
            TempData["userId"] = id;
            //var result = (from x in _debitService.GetList().ToList()
            //              join c in _categoryService.GetList().ToList() on x.CategoryId equals c.Id
            //              select new
            //              {
                              
            //                  x.ProductName,
            //                  x.ProductDetail,
            //                  c.CategoryName
            //              }).ToList();
            //List<RequestDebitVmDto> debit = new List<RequestDebitVmDto>();
            //foreach (var item in result)
            //{
            //    debit.Add(new RequestDebitVmDto()
            //    {

            //        ProductName = item.ProductName,
            //        ProductDetail = item.ProductDetail,
            //        CategoryName = item.CategoryName


            //    });
            //}

            //join kısmında eksik olabilir, post methodunda userdebitdto ya verileri aktarmada sorun yasadık.
                return View(/*debit*/);
        }
        [HttpPost]
        public IActionResult AddDebit(RequestDebitVmDto requestDebitVmDto)
        {
            
            AppUser user = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;
            UserDebitDto userdebitdto = new UserDebitDto();


            userdebitdto.StartDate = requestDebitVmDto.StartDate;
            userdebitdto.EndDate = requestDebitVmDto.EndDate;
            userdebitdto.DebitId = requestDebitVmDto.DebitId;
            userdebitdto.IsReceived = false;
            userdebitdto.UserId = user.Id;
            
            
            _userDebitService.Add(userdebitdto);
            return View();
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
