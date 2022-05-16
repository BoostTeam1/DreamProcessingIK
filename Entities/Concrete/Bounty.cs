using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Bounty:BaseEntity
    {
        public Bounty()
        {
            UserBountyDtos = new HashSet<UserBountyDto>();
        }
        public decimal? Amount { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserBountyDto> UserBountyDtos { get; set; }



    }
}
