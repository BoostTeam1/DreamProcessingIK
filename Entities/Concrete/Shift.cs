using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Shift : BaseEntity
    {
        public Shift()
        {
            UserShiftBreakDtos = new HashSet<UserShiftBreakDto>();
        }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; } = DateTime.Today.AddHours(8);
        public DateTime? EndDate { get; set; } = DateTime.Today.AddHours(16);
        public virtual ICollection<UserShiftBreakDto> UserShiftBreakDtos { get; set; }
    }
}
