using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CompanyDateDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public DateTime Birthday { get; set; }
    }
}
