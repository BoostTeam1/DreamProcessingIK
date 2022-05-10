using Business.Abstract;
using Entities.Concrete;
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
        public  async Task<IActionResult> RequestVocation()
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
                AppUser app =  await _userManager.FindByIdAsync(ViewBag.userıd);
                IList<string> rolesa = await _userManager.GetRolesAsync(app);
                foreach (var item in rolesa)
                {
                    if (item.Contains("Manager"))
                    {
                        ViewBag.managerId=app.Id;
                    }
                }
            


            }

            return View();
        }
        [HttpPost]
        public  IActionResult RequestVocation(UserVacationDto userVacationDto)
        {
            List<UserVacationDto> returnData = new List<UserVacationDto>();
            returnData.Add(new UserVacationDto()
            {
                UserId = ViewBag.employeeıd,

            });


            return View(userVacationDto);
        }



    }
}
