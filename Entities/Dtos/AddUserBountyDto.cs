using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class AddUserBountyDto
    {
        public string UserId { get; set; }
        public int BountyId { get; set; }
        public string FullName { get; set; }
        public  decimal Amount { get; set; }
        public string Description { get; set; }

        public  short ConstantSalary { get; set; }
        public decimal Total { get; set; }

    }
}
