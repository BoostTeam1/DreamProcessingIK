using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserVacationDto:BaseEntity
    {
        public string UserId { get; set; }
        public int? HolidayId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsConfirmed { get; set; }
        
        public virtual Vacation Vacation { get; set; }
        public virtual AppUser User { get; set; }
    }
}
