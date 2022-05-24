using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class ManagerControlPublicHoliday : ViewComponent
    {
        public UserManager<AppUser> _userManager { get; set; }
        private readonly IVacationService _vacationService;
        private readonly IUserVacationService _userVacationService;
        public ManagerControlPublicHoliday(IVacationService vacationService, IUserVacationService userVacationService, UserManager<AppUser> userManager)
        {
            _vacationService = vacationService;
            _userManager = userManager; 
            _userVacationService = userVacationService;
        }

        public IViewComponentResult Invoke()
        {
            AppUser usera = _userManager.FindByNameAsync("coymax00").Result;
            //var companyFind = _userCompanyService.GetByUserId(usera.Id);




            var result = (from uv in _userVacationService.GetList().Where(x => x.UserId == usera.Id).ToList()
                          join v in _vacationService.GetList().Where(x => x.Title.ToLower() == "resmi").ToList() on uv.HolidayId equals v.Id
                          select new
                          {
                              Name = v.Name,
                              StartDate = (System.DateTime)uv.StartDate,
                              EndDate = (System.DateTime)uv.EndDate
                          }).ToList();

            List<PublicVacationsDto> publicVacations = new List<PublicVacationsDto>();
            foreach (var date in result)
            {
                publicVacations.Add(new PublicVacationsDto()
                {
                    Name = date.Name,
                    StartDate = date.StartDate,
                    EndDate = date.EndDate
                });
            }
            return View(publicVacations);
        }
    }
}
