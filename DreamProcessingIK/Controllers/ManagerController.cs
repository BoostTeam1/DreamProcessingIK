using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DreamProcessingIK.Controllers
{
    public class ManagerController : Controller
    {
     
        public UserManager<AppUser> _userManager;
        public RoleManager<AppRole> _roleManager;
        public SignInManager<AppUser> _signInManager;
        private readonly IUserCompanyService _userCompanyService;

        public ManagerController( RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IUserCompanyService userCompanyService)
        {
            _userCompanyService = userCompanyService;
            _roleManager = roleManager;
     
            _signInManager = signInManager;
            _userManager = userManager;


        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddEmployee()
        {
      
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> AddEmployee(EmployeeAddDto employeeAddDto,UserCompanyDto userCompanyDto)
        {
            AppUser user = new AppUser();
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;
            ViewBag.userID = usera.Id;
            user.FirstName=employeeAddDto.FirstName;
            user.UserName=employeeAddDto.UserName;
            user.Email=employeeAddDto.Email;
            user.LastName=employeeAddDto.LastName;
            user.IsConfirmed = employeeAddDto.IsConfirmed;
            user.EmailConfirmed=employeeAddDto.EmailConfirmed;
            IdentityResult result = await _userManager.CreateAsync(user, employeeAddDto.Password);
       
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Personel");
                var managerCompany=  _userCompanyService.GetByUserId(ViewBag.userID);
                employeeAddDto.UserId=user.Id;
                userCompanyDto.UserId = employeeAddDto.UserId;
                userCompanyDto.CompanyId=managerCompany.CompanyId;
                _userCompanyService.Add(userCompanyDto);

                ViewBag.succeeded = "true";
            }
            else
            {
                AddModelError(result);
            }
            return View(employeeAddDto);
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
