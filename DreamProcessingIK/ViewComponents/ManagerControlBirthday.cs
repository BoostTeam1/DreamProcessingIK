using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class ManagerControlBirthday: ViewComponent
    {
        public UserManager<AppUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly IUserCompanyService _userCompanyService;

        public ManagerControlBirthday(UserManager<AppUser> user, ICompanyService companyService, IUserCompanyService userCompanyService)
        {
            _userCompanyService = userCompanyService;
            _companyService = companyService;
            _userManager = user;
        }

        public IViewComponentResult Invoke()
        {
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;
          

        
            var companyFind = _userCompanyService.GetByUserId(usera.Id);
            


        
           var result = (from x in _userCompanyService.GetList().ToList()
                          join u in _userManager.Users.ToList() on x.UserId equals u.Id 
                          select new 
                          {
                           
                            CompanyId= (int)x.CompanyId,
                            Birthday= (System.DateTime)u.BirthDate,
                            Name = u.FirstName + " " + u.LastName

                          }).ToList();
            List<CompanyDateDto> company = new List<CompanyDateDto>();
            foreach (var date in result)
            {
                company.Add(new CompanyDateDto()
                {
                    Name = date.Name,
                    Birthday = date.Birthday
                });

            }



            return View(result);
        }

    }
}
