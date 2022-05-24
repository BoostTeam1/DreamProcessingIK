using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace DreamProcessingIK.ViewComponents
{
    public class ManagerControlCostList : ViewComponent
    {
        public UserManager<AppUser> _userManager;
        private readonly ICostService _costService;
        private readonly IUserCostService _userCostService;

        public ManagerControlCostList(UserManager<AppUser> user, ICostService costService, IUserCostService userCostService)
        {
            _userManager = user;
            _costService = costService; 
            _userCostService = userCostService; 
        }

        public IViewComponentResult Invoke()
        {
            AppUser usera = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var result = (from x in _userCostService.GetList().Where(x => x.UserId == usera.Id).ToList()
                          join u in _costService.GetList().ToList() on x.CostId equals u.Id
                          select new
                          {
                              Name = u.Name,
                              PaymentDate = (System.DateTime)u.PaymentDate,
                              CostPrice = u.CostPrice
                          }).ToList();
            List<CompanyCostDto> costList = new List<CompanyCostDto>();
            foreach (var cost in result)
            {
                costList.Add(new CompanyCostDto()
                {
                    Name = cost.Name,
                    PaymentDate = cost.PaymentDate,
                    CostPrice = cost.CostPrice
                });
            }

            return View(costList);
        }
    }
       
}
