using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
public class AddBountyDto
    {
        public decimal? Amount { get; set; }
        public string Description { get; set; }
    }
}
