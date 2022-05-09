using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Sector:BaseEntity
    {
        public Sector()
        {
            CompanySectors = new HashSet<CompanySectorDto>();
        }
        public string Name { get; set; }
        public bool Exist { get; set; }
        public virtual ICollection<CompanySectorDto> CompanySectors { get; set; }

    }
}
