using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CompanySectorListDto
    {
        public List<Company> Companies{ get; set; }
        public List<Sector> Sectors { get; set; }
        
    }
}
