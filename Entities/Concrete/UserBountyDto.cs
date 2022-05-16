using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserBountyDto:BaseEntity
    {
        public string UserId { get; set; }
        public int BountyId { get; set; }
        public virtual Bounty Bounty { get; set; }
        public virtual AppUser User { get; set; }


    }
}
