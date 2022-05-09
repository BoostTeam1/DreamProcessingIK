using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CreateVacationUserDto
    {
        public string UserId { get; set; } 
        public int VacationId { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public List<Vacation> Vacations { get; set; }
        public List<AppUser> Users { get; set; }
    }
}
