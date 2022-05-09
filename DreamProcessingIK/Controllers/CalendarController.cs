using Business.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly IUserVacationService _userVacationService;
        private readonly IVacationService _vacationService;
        public UserManager<AppUser> _userManager;

        public CalendarController(IEventService eventService, IUserVacationService userVacationService, IUserService userService, IVacationService vacationService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _userService = userService;
            _userVacationService = userVacationService;
            _vacationService = vacationService;
            _eventService = eventService;
        }

        public IActionResult Calendar()
        {
            MyContext db = new MyContext();

            return View();
        }
        public IActionResult ViewCalendar()
        {
           
            var Events = 
            (
                 from uv in _userVacationService.GetList().ToList()
                          join v in _vacationService.GetList().ToList() on uv.HolidayId equals v.Id
                          join u in _userManager.Users.ToList() on uv.UserId equals u.Id
                 select new Eventt
                 {
                     Color = v.Color,
                     End = uv.EndDate.Value,
                     Start = uv.StartDate.Value,
                     TextColor = v.Color,
                     Title = u.FirstName.ToUpper() + " " + u.LastName.ToUpper() + " " + v.Name
                 }
            ).ToList();
            //foreach (var item in Events)
            //{
            //    if (item.Title.Contains("Bayram"))
            //    {
            //        item.Color = "#CCFFFF";
            //        item.TextColor = "#FFFFCC";
            //    }
            //    else if (item.Title.Contains("Yılbaşı"))
            //    {
            //        item.Color = "#FFCCE5";
            //        item.TextColor = "#FFCCE5";
            //    }
            //    else if (item.Title.Contains("Doğum Günü"))
            //    {
            //        item.Color = "#330000";
            //        item.TextColor = "#330000";
            //    }
            //}
            //var events = _eventService.GetList();
            return new JsonResult(Events);
        }

    }
}
