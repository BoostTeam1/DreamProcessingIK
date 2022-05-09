using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Debits = new HashSet<Debit>();
        }
        public string CategoryName { get; set; }

        public virtual ICollection<Debit> Debits { get; set; }
    }
}
