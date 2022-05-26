using Business.Abstract;
using Core.Helper;
using Entities.Concrete;
using Entities.Dtos;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
        private readonly IPersonnelDocumentService _personnelDocumentService;
        private readonly IUserShiftService _userShiftService;
        private readonly IBountyService _bountyService;
        private readonly IUserBountyService _userBountyService;
        public ManagerController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserCompanyService userCompanyService, IUserVacationService userVacationService, IUserDebitService userDebitService, IDebitService debitService, ICategoryService categoryService, IVacationService vacationService,IShiftService shiftService,IBreakService breakService,IUserShiftBreakService userShiftBreakService, IPersonnelDocumentService personnelDocumentService,IUserShiftService userShiftService,IBountyService bountyService,IUserBountyService userBountyService)
        {
            _userBountyService = userBountyService;
            _bountyService = bountyService;
            _userShiftService = userShiftService;
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
            _personnelDocumentService = personnelDocumentService;

            UpdateShiftDates(); //düzenlenebilir
        }
        public IActionResult SendMessageAllUsers()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public void UpdateShiftDates()
        {
            List<Shift> result = _shiftService.GetList();
            foreach (var item in result)
            {
                int startTime, endTime;
                startTime = Convert.ToInt32(item.StartDate.Value.ToString("HH"));
                endTime = Convert.ToInt32(item.EndDate.Value.ToString("HH"));
                Shift shift = _shiftService.GetById(item.Id);
                var startDate = DateTime.Now.Date.Add(new TimeSpan(startTime, 0, 0));
                var endDate = DateTime.Now.Date.Add(new TimeSpan(endTime, 0, 0));
                shift.StartDate = startDate;
                shift.EndDate = endDate;
                _shiftService.Update(shift);
            }
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
        public IActionResult CompanyEmployee() //personelleri listeleme tarafı
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
            userdebitdto.EndDate = userdebitdto.StartDate.Value.AddYears(1);
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
                              ShiftId = s.Id,
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

        public IActionResult PersonnelDocumentList()
        {
            List<PersonnelDocumentListDto> documentList = new List<PersonnelDocumentListDto>(); 
            foreach (var item in _personnelDocumentService.GetList())
            {
                AppUser appUser = _userManager.FindByIdAsync(item.AppUserId).Result;
                PersonnelDocumentListDto document = new PersonnelDocumentListDto();
                document.DocumentId = item.Id;
                document.FirstName = appUser.FirstName;
                document.LastName = appUser.LastName;
                document.FileName = item.FileName;
                document.FileDetails = item.FileDetails;
                document.FileGeneratedDate = item.FileGeneratedDate;
                documentList.Add(document);    
            }
            return View(documentList);
        }


        public IActionResult AddPersonnelDocument(string id)
        {
                   
            TempData["userId"] = id;
                       
            return View();  
        }
        [HttpPost]
        public IActionResult AddPersonnelDocument(PersonnelDocuments personelDocument)
        {
            personelDocument.AppUserId = TempData["userId"].ToString();
            _personnelDocumentService.Add(personelDocument);
            return View();
        }
        public IActionResult UpdatePersonnelDocument(int id)
        {
            TempData["documentId"] = id;
            TempData["userId"] = _personnelDocumentService.GetById(id).AppUserId;
            return View(_personnelDocumentService.GetById(id));
        }
        [HttpPost]
        public IActionResult UpdatePersonnelDocument(PersonnelDocuments personelDocument)
        {
            personelDocument.Id = (int)TempData["documentId"];
            personelDocument.AppUserId = TempData["userId"].ToString();
            _personnelDocumentService.Update(personelDocument);
            return RedirectToAction("PersonnelDocumentList", "Manager");
        }
        public IActionResult DeletePersonnelDocument(int id)
        {
            _personnelDocumentService.Delete(_personnelDocumentService.GetById(id));
            return RedirectToAction("PersonnelDocumentList", "Manager");
        }

        public IActionResult InActiveShiftBreakEmployee(string id)
        {
            // buton çalışıyor ListemployeeCompany de ısactıve false dönüyor
            var user = _userShiftBreakService.GetByUserId(id);
            

            if (user.IsActive == true)
            {
                TempData["active"] = "false";
                user.IsActive = false;

            }
            else
            {
                TempData["active"] = "true";
                user.IsActive = true;
            }
            


            _userShiftBreakService.Update(user);


            return RedirectToAction("ListEmployeeCompany", "Manager");
        }
        public IActionResult ListEmployeeCompany() //personel listesi için kontrol et 
        {
            AppUser user = new AppUser();
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var companyFind = _userCompanyService.GetByUserId(usera.Id);
            


            var companyList = _userCompanyService.GetList();
            var result = (from x in companyList.ToList()
                          join u in _userManager.Users.ToList() on x.UserId equals u.Id
                          join s in _userShiftService.GetList().ToList() on x.UserId equals s.UserId
                          join h in _shiftService.GetList().ToList() on s.ShiftId equals h.Id
                          join sb in _userShiftBreakService.GetList().ToList() on u.Id equals sb.UserId
                          join b in _breakService.GetList().ToList() on sb.BreakId equals b.Id
                          select new
                          {
                              x.UserId,
                              u.FirstName,
                              u.LastName,
                              u.IsConfirmed,
                              x.CompanyId,
                              s.ShiftId,
                              shiftStartDate = h.StartDate,
                              shiftEndDate = h.EndDate,
                              sb.BreakId,
                              breakStartDate = b.StartDate,
                              breakEndDate = b.EndDate



                          }).Where(x => x.CompanyId == companyFind.CompanyId && x.IsConfirmed == true).ToList();
            List<EmployeeListForShiftDto> employeeLists = new List<EmployeeListForShiftDto>();

            foreach (var item in result)
            {

                if (usera.Id != item.UserId)
                {
                    employeeLists.Add(new EmployeeListForShiftDto()
                    {
                        UserId = item.UserId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        ShiftStartDate = (System.DateTime)item.shiftStartDate,
                        ShiftEndDate = (System.DateTime)item.shiftEndDate,
                        BreakStartDate = (System.DateTime)item.breakStartDate,
                        BreakEndDate = (System.DateTime)item.breakEndDate

                    });

                }
            }


            return View(employeeLists);
        }
        [HttpGet]
        public IActionResult AddShiftEmployee(string id)
        {
            TempData["userId"] = id;
            Dictionary<int, string> saatler = new Dictionary<int, string>();
            if (saatler.Count == 0)
            {
                saatler.Add(1, "1 saat");
                saatler.Add(2, "2 saat");
                saatler.Add(3, "3 saat");
                saatler.Add(4, "4 saat");

                ViewBag.Paket = new SelectList(saatler, "Key", "Value");
            }

            return View();

        }

        [HttpPost]
        public IActionResult AddShiftEmployee(AddShiftEmployeeDto addShiftEmployeeDto)
        {


      
            var usershift = _userShiftService.GetByUserId(TempData["userId"].ToString());
            var userFind = _userManager.FindByIdAsync(TempData["userId"].ToString());
            var result = (from x in _shiftService.GetList().ToList()
                          join s in _userShiftService.GetList().ToList() on x.Id equals s.ShiftId
                          select new
                          {
                              s.UserId,
                              x.EndDate
                          }).Where(x => x.UserId == TempData["userId"].ToString()).ToList();
            Shift shift = _shiftService.GetById(usershift.ShiftId);
            shift.EndDate = shift.EndDate.Value.AddHours(addShiftEmployeeDto.HourTime);
            _shiftService.Update(shift);

            return RedirectToAction("ListEmployeeCompany", "Manager");
        }
        [HttpGet]
        public IActionResult AddBreakEmployee(string id)
        {
            TempData["userBreakId"] = id;
            Dictionary<int, string> saatler = new Dictionary<int, string>();
            if (saatler.Count == 0)
            {
                saatler.Add(1, "1 saat");
                saatler.Add(2, "2 saat");
                saatler.Add(3, "3 saat");
                saatler.Add(4, "4 saat");

                ViewBag.Paket = new SelectList(saatler, "Key", "Value");
            }
            return View();
        }

        public IActionResult AddBreakEmployee(AddBreakEmployeeDto addBreakEmployeeDto)
        {


            var userShiftbreak = _userShiftBreakService.GetByUserId(TempData["userBreakId"].ToString());
            var userFind = _userManager.FindByIdAsync(TempData["userBreakId"].ToString());
            var result = (from x in _breakService.GetList().ToList()
                          join s in _userShiftBreakService.GetList().ToList() on x.Id equals s.BreakId
                          select new
                          {
                              s.UserId,
                              x.EndDate
                          }).Where(x => x.UserId == TempData["userBreakId"].ToString()).ToList();
            Break breaks = _breakService.GetById(userShiftbreak.Id);
            breaks.EndDate = breaks.EndDate.Value.AddHours(addBreakEmployeeDto.HourTime);
            _breakService.Update(breaks);
            return RedirectToAction("ListEmployeeCompany", "Manager");
        }
          
        
        



        public IActionResult _ManagerDashboard()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }
       
        public IActionResult AddBounty()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBounty(AddBountyDto addBountyDto) //link bağlanacak
        {
            Bounty bounty = new Bounty();
            bounty.Amount = addBountyDto.Amount;
            bounty.Description = addBountyDto.Description;
            _bountyService.Add(bounty);
            
            return View(addBountyDto);
        }
  

        public IActionResult UserBountyList()
        {
            //var bountyType= _bountyService.GetList().ToList();
            //ViewBag.BountyType=bountyType;

           List<AddUserBountyDto>  addUserBounty = new List<AddUserBountyDto>();

            //AppUser userFind = _userManager.FindByIdAsync(id).Result;
            //var result = _userBountyService
      
                var resultListBounty = (from u in _userManager.Users.ToList()
                                        join ub in _userBountyService.GetList().ToList() on u.Id equals ub.UserId
                                        join b in _bountyService.GetList().ToList() on ub.BountyId equals b.Id
                                        select new
                                        {
                                            u.Id,
                                            FullName = u.FirstName + " " + u.LastName,
                                            u.ConstantSalary,
                                            ub.BountyId,
                                            BountyType = b.Id,
                                            b.Amount,
                                            b.Description

                                        }).ToList();

                foreach (var item in  resultListBounty)
                {
                    addUserBounty.Add(new AddUserBountyDto()
                    {
                        FullName= item.FullName,
                        ConstantSalary= (short)item.ConstantSalary,
                        Amount= (decimal)item.Amount,
                        Description= item.Description,
                        Total= (decimal)(item.ConstantSalary+item.Amount)



                    });
                }
         

       
            
            return View(addUserBounty);

        }
        [HttpPost]
        public IActionResult AddUserBounty()
        {

            return View();
        }


        public IActionResult AddComment()
        {
            AppUser appUser = _userManager.GetUserAsync(User).Result;
            return View(appUser);
        }

        [HttpPost]
        public IActionResult AddComment(AppUser appUser)
        {
            if (appUser.Comment != null)
            {
                _userManager.UpdateAsync(appUser);
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Herhangi bir yorum girmediniz. Lütfen yorumunuzu yazınız");
                return View();
            }
        }
    }
}
