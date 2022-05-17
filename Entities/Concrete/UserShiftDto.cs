using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
   public class UserShiftDto:BaseEntity
    {
        public string UserId { get; set; }
        public int ShiftId { get; set; }
        public virtual Shift Shifts { get; set; }
        public  virtual AppUser User { get; set; }




    }
}
