using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Debit : BaseEntity
    {
        public Debit()
        {
            UserDebitDtos = new HashSet<UserDebitDto>();
        }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string ProductDetail { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<UserDebitDto> UserDebitDtos { get; set; }
    }
}
