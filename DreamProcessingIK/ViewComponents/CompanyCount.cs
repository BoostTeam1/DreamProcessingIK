using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace DreamProcessingIK.ViewComponents
{
    public class CompanyCount: ViewComponent
    {
        private readonly ICompanyService _companyService;

        public CompanyCount(ICompanyService companyService)
        {
            _companyService=companyService;
        }
        public IViewComponentResult Invoke()
        {
          var result= _companyService.GetList();
            return View(result);
        }

    }
}
