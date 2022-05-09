using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class PersonelCount : ViewComponent
    {
        public UserManager<AppUser> _userManager;
        public PersonelCount(UserManager<AppUser> user)
        {
            _userManager = user;
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

            return View(users);
        }
    }
}
