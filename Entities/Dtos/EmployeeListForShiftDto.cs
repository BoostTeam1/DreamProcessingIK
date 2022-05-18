using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
public class EmployeeListForShiftDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ShiftStartDate { get; set; }
        public DateTime ShiftEndDate { get; set; }
        public DateTime BreakStartDate { get; set; }
        public DateTime BreakEndDate { get; set; }
        public bool ısActive { get; set; }


    }
}
