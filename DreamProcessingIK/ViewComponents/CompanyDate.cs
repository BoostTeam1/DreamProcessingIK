using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class CompanyDate:ViewComponent
    {
        public UserManager<AppUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly  IUserCompanyService _userCompanyService;

        public CompanyDate(UserManager<AppUser> user,ICompanyService companyService,IUserCompanyService userCompanyService)
        {
            _userCompanyService=userCompanyService;
            _companyService = companyService;
            _userManager = user;
        }
        public IViewComponentResult Invoke()
        {
            List<CompanyDateDto> result = (from x in _userManager.Users.ToList()
                         join uc in _userCompanyService.GetList().ToList()
                         on x.Id equals uc.UserId
                         join c in _companyService.GetList().ToList()
                         on uc.CompanyId equals c.Id
                         select new CompanyDateDto
                         {
                             CompanyName = c.CompanyName,
                             Name = x.FirstName + " " + x.LastName,
                             Start = c.StartDate.Value,
                             End = c.EndDate.Value
                         }).ToList();
            return View(result);
        }
    }
}
