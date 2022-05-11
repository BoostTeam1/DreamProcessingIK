using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
  public class RequestDebitVmDto
    {

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public string UserId { get; set; }
        public int? DebitId { get; set; }
        public bool? IsReceived { get; set; }
        public string ManagerApprovedId { get; set; }
    }
}
