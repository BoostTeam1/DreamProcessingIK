using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CompanyCostDto
    {
        public string Name { get; set; }
        public short? CostPrice { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
