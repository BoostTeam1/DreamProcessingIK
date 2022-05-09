using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Break : BaseEntity
    {
        public Break()
        {
            UserShiftBreakDtos = new HashSet<UserShiftBreakDto>();
        }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual ICollection<UserShiftBreakDto> UserShiftBreakDtos { get; set; }
    }
}
