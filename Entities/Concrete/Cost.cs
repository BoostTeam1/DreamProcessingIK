using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Cost : BaseEntity
    {
        public Cost()
        {
            UserCostDto = new HashSet<UserCostDto>();
        }
        public string Name { get; set; }
        public  short? CostPrice { get; set; }
        public short? WagePerHour { get; set; } //shift
        public string Image { get; set; }
        public bool? IsConfirmed { get; set; }

        public virtual ICollection<UserCostDto> UserCostDto { get; set; }
    }
}
