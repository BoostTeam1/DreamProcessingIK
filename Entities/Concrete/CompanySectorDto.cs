using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
   public class CompanySectorDto:BaseEntity
    {
        public int  CompanyId { get; set; }
        public int SectorId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
