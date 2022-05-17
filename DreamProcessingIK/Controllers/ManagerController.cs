using Business.Abstract;
using Core.Helper;
using Entities.Concrete;
using Entities.Dtos;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IVacationService _vacationService;
        private readonly IUserVacationService _userVacationService;
        private readonly IUserDebitService _userDebitService;
        private readonly IDebitService _debitService;
        private readonly ICategoryService _categoryService;
        private readonly IShiftService _shiftService;
        private readonly IBreakService _breakService;
        private readonly IUserShiftBreakService _userShiftBreakService;
        public ManagerController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserCompanyService userCompanyService, IUserVacationService userVacationService, IUserDebitService userDebitService, IDebitService debitService, ICategoryService categoryService, IVacationService vacationService,IShiftService shiftService,IBreakService breakService,IUserShiftBreakService userShiftBreakService)
        {
            _userShiftBreakService = userShiftBreakService;
            _breakService = breakService;

            _shiftService = shiftService;
            _categoryService = categoryService;
            _debitService = debitService;
            _userDebitService = userDebitService;
            _userVacationService = userVacationService;
            _userCompanyService = userCompanyService;
            _roleManager = roleManager;

            _signInManager = signInManager;
            _userManager = userManager;
            _vacationService = vacationService;

        }


        public IActionResult Index()
        {
            return View();
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
        public IActionResult EmployeeEdit(UserEditDto userEditDto, string id)
        {
            AppUser user = _userManager.FindByIdAsync(id).Result;
            UserEditDto userEdit = user.Adapt<UserEditDto>();
            TempData["userId"] = id;


            return View(userEdit);
        }
        [HttpPost]
        public async Task<IActionResult> EmployeeEdit(UserEditDto userEditDto)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                AppUser user = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;
                user.UserName = userEditDto.UserName;
                user.FirstName = userEditDto.FirstName;
                user.LastName = userEditDto.LastName;
                user.PhoneNumber = userEditDto.PhoneNumber;
                user.BirthPlace = userEditDto.BirthPlace;
                user.BirthDate = userEditDto.BirthDate;
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);

                }
                else
                {
                    AddModelError(result);
                }
            }

            return View(userEditDto);
        }
        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            TempData["userId"] = id;

            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;
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

        public async Task<IActionResult> PassiveEmployee(string id)
        {
            AppUser user = _userManager.FindByIdAsync(id).Result;


            if (user.IsConfirmed == true)
            {
                user.IsConfirmed = false;

            }
            else if (user.IsConfirmed == false)
            {
                user.IsConfirmed = true;
            }
            IdentityResult passiveResult = await _userManager.UpdateAsync(user);
            if (passiveResult.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);

            }
            return RedirectToAction("CompanyEmployee", "Manager");

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
                                join u in _userManager.Users.ToList() on x.UserId equals u.Id
                                join v in _vacationService.GetList().ToList() on x.HolidayId equals v.Id
                                select new
                                {
                                    x.ManagerApprovedId,
                                    x.UserId,
                                    İsim = u.FirstName + " " + u.LastName,
                                    OnayDurumu = x.IsConfirmed,
                                    x.HolidayId,
                                    v.Name
                                }).ToList();
            List<VacationConfirmedDto> vacationConfirmedDto = new List<VacationConfirmedDto>();

            foreach (var item in findUserVoca)
            {
                if (item.ManagerApprovedId == usera.Id)
                {
                    vacationConfirmedDto.Add(new VacationConfirmedDto()
                    {

                        HolidayId = (int)item.HolidayId,
                        FullName = item.İsim,
                        UserId = item.UserId,
                        IsConfirmed = (bool)item.OnayDurumu,
                        Name = item.Name

                    });


                }

            }
            return View(vacationConfirmedDto);
        }

        [HttpPost]
        public IActionResult ApprovedVacation(VacationConfirmedDto vacationConfirmedDto)
        {



            return View();
        }

        [HttpPost]
        public IActionResult ApprovedVocation()
        {



            return View();
        }


        public IActionResult VacationConfirm(string id)
        {

            TempData["userId"] = id;
            var userFind = _userManager.FindByIdAsync(TempData["userId"].ToString()).Result;
            //deneme
            //deneme2
            var result = _userVacationService.GetList();
            foreach (var item in result)
            {
                // UserVacationDto userVacationDto = _userVacationService.GetByVacationId((int)item.HolidayId);

                if (item.UserId == userFind.Id)
                {
                    if (item.IsConfirmed == false)
                    {
                        item.IsConfirmed = true;


                    }
                    else if (item.IsConfirmed == true)
                    {
                        item.IsConfirmed = false;

                    }
                    _userVacationService.Update(item);

                }

            }

            return RedirectToAction("ApprovedVacation", "Manager");
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
                    UserId = item.UserId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    CategoryName = item.CategoryName,
                    ProductName = item.ProductName,
                    ProductDetail = item.ProductDetail,

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
                              u.IsConfirmed,
                              x.CompanyId

                          }).Where(x => x.CompanyId == companyFind.CompanyId).ToList();
            List<EmployeeListCompanyDto> employeeLists = new List<EmployeeListCompanyDto>();

            foreach (var item in result)
            {
                if (usera.Id != item.UserId)
                {
                    employeeLists.Add(new EmployeeListCompanyDto()
                    {
                        UserId = item.UserId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        IsConfirmed = item.IsConfirmed
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
            //                  x.Id,
            //                  x.ProductName,
            //                  x.ProductDetail,
            //                  x.CategoryId,
            //                  x.Category.CategoryName
            //              }).ToList();

            var debit = _debitService.GetList().ToList();
            var category = _categoryService.GetList().ToList();
            Dictionary<string, string> product = new Dictionary<string, string>();
            Dictionary<string, string> categoryList = new Dictionary<string, string>();

            foreach (var item in debit)
            {
                if (!product.ContainsKey(item.Id.ToString()))
                {
                    product.Add(item.Id.ToString(), item.ProductName);
                }
            }
            ViewBag.Product = new SelectList(product, "Key", "Value");
            foreach (var item in category)
            {
                if (!categoryList.ContainsKey(item.Id.ToString()))
                {
                    categoryList.Add(item.Id.ToString(), item.CategoryName);
                }
            }
            ViewBag.Category = new SelectList(categoryList, "Key", "Value");

            return View();
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
            ManagerConfirmDebitHelper.ManagerConfirmEmail(user.Email);

            return View();
        }
        public IActionResult DebitList()
        {

            return View(_debitService.GetList().ToList());
        }

        public IActionResult CreateDebit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDebit(Debit debit)
        {
            _debitService.Add(debit);
            return View();
        }

        public IActionResult CreateShift()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateShift(Shift shift)
        {
            _shiftService.Add(shift);
            return View();
        }
        public IActionResult CreateBreak()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBreak(Break breaks)
        {
            _breakService.Add(breaks);
            
            return View();
        }
        public IActionResult ListBreakShift()
        {
            AppUser user = new AppUser();
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var result = (from x in _userShiftBreakService.GetList().ToList()
                          join u in _userManager.Users.ToList() on x.UserId equals u.Id
                          join b in _breakService.GetList().ToList() on x.BreakId equals b.Id
                          join s in _shiftService.GetList().ToList() on x.ShiftId equals s.Id
                          select new
                          {
                              UserId=u.Id,
                              Fullname = u.FirstName + " " + u.LastName,
                              BreaksId = b.Id,
                              BreaksName = b.Name,
                              s.Id,
                              s.Name,
                              s.StartDate,
                              s.EndDate,
                              BreaksStart=b.StartDate,
                              BreaksEnd=b.EndDate,
                              x.ManagerApprovedId



                          }).ToList();
            List<BreakShiftListDto> list = new List<BreakShiftListDto>();
            //Dictionary<int, string> breaksList = new Dictionary<int, string>();
            //Dictionary<int, string> shiftList = new Dictionary<int, string>();
            foreach (var item in result)
            {
                if (item.ManagerApprovedId==usera.Id)
                {
                    list.Add(new BreakShiftListDto()
                    {
                        UserId = item.UserId,
                        FullName= item.Fullname,
                        ShiftName=item.Name,
                        BreaksName=item.Name,
                        BreakStartDate= (System.DateTime)item.BreaksStart,
                        BreakEndDate= (System.DateTime)item.BreaksEnd,
                        ShiftStartDate= (System.DateTime)item.StartDate,
                        ShiftEndDate= (System.DateTime)item.EndDate,

                        
                        
                      
                    });
                    //breaksList.Add(item.BreaksId, item.BreaksName);
                    //shiftList.Add(item.Id, item.Name);
             
                }

            }
            //ViewBag.breaksSelect = new SelectList(breaksList, "Key", "Value");
            //ViewBag.shiftSelect = new SelectList(shiftList, "Key", "Value");

 
            return View(list);
        }





        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }
        }

        public IActionResult AddPersonnelDocument(string id)
        {
                   
            TempData["userId"] = id;
                       
            return View();  
        }
        [HttpPost]
        public IActionResult AddPersonnelDocument(PersonnelDocuments personelDocument)
        {
            
            return View();
        }
        public IActionResult _ManagerDashboard()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }


    }
}
