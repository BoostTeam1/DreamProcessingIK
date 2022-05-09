using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class DashboardUserCount : ViewComponent
    {
        public UserManager<AppUser> _userManager;
        public DashboardUserCount(UserManager<AppUser> user)
        {
            _userManager = user;
        }
        public IViewComponentResult Invoke()
        {

            List<AppUser> result = _userManager.Users.ToList();
            
            return View(result);
        }
    }
}
