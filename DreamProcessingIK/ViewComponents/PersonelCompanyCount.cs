using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class PersonelCompanyCount : ViewComponent
    {
        public UserManager<AppUser> _userManager;
        private readonly ICompanyService _companyService;
        public PersonelCompanyCount(UserManager<AppUser> user,ICompanyService companyService)
        {
            _userManager = user;
            _companyService = companyService;  
        }
        public IViewComponentResult Invoke()
        {
       
            List<AppUser> result = _userManager.Users.ToList();
            List<AppUser> users = new List<AppUser>();
            foreach (var item in result)
            {
                List<string> userRoles = _userManager.GetRolesAsync(item).Result as List<string>;
                if (userRoles.Contains("Personel") && item.EmailConfirmed == true && item.IsConfirmed == true)
                {
                    users.Add(item);
            
                  
                }
            }
            var resultcompany = _companyService.GetList();
            var total = users.Count() + resultcompany.Count();
            ViewBag.totalusers=total;


            return View();
        }
    }
}
