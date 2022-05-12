using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
  public class UserDebitDto : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? DebitId { get; set; }
        public bool? IsReceived { get; set; }
        public string ManagerApprovedId { get; set; }
        public virtual Debit Debit { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Category Category { get; set; }
    }
}
