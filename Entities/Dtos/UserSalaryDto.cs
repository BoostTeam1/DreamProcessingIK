using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserSalaryDto
    {
        public decimal TotalSalary { get; set; }
        public AppUser AppUser { get; set; }
        public Bounty Bounty { get; set; }
        public Cost Cost { get; set; }
    }
}
