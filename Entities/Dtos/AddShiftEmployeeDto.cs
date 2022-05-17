using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class AddShiftEmployeeDto
    {
        public string UserId { get; set; }
        public DateTime EndDate { get; set; }
        public int HourTime { get; set; }
        public string FullName { get; set; }
 
        public void  AddHour()
        {
            EndDate.AddHours(HourTime);
        }
            
    }
}
