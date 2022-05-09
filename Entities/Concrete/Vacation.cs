using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Vacation : BaseEntity
    {
        public Vacation()
        {
            UserVacationDtos = new HashSet<UserVacationDto>();
        }
        public string Name { get; set; }
        public string Color { get; set; }
        public virtual ICollection<UserVacationDto> UserVacationDtos { get; set; }

    }
}
