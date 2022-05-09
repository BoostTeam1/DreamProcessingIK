using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
   public class UserShiftBreakDto:BaseEntity
    {
        public string UserId { get; set; }
        public int? ShiftId { get; set; }
        public int? BreakId { get; set; }

        public virtual Break Break { get; set; }
        public virtual Shift Shift { get; set; }
        public virtual AppUser User { get; set; }
    }
}
