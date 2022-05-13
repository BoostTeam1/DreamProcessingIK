using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class VacationConfirmedDto
    {
        public string ManagerApprovedId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public int HolidayId { get; set; }
        public bool IsConfirmed { get; set; }

    }
}
