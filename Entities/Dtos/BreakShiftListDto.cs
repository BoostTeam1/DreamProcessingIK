using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
 public class BreakShiftListDto
    {
        public string  UserId { get; set; }
        public string FullName { get; set; }
        public int BreakId { get; set; }
        public int ShiftId { get; set; }
        public string BreaksName   {get; set; }
        public DateTime BreakStartDate { get; set; }
        public DateTime ShiftStartDate { get; set; }
        public DateTime BreakEndDate { get; set; }
        public DateTime ShiftEndDate { get; set; }
        public string ShiftName { get; set; }


    }
}
