using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
   public class CreateCompanyForAdminDto
    {
        public string CompanyName { get; set; }
        public string SectorName { get; set; }
        public string CompanyDescription { get; set; }
        public IFormFile Logo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Sector> Sectors { get; set; }
    }
}
