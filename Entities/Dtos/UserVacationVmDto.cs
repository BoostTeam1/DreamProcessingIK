using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
public class UserVacationVmDto
    {

        public string UserId { get; set; }
        public string ApprovedManagerId { get; set; }
        public int? HolidayId { get; set; }
        public bool? Exist { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsConfirmed { get; set; }
   
    }
}
