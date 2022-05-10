using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
   public class EmployeeAddDto:AppUser
    {
        public bool IsConfirmed { get; set; } = true;
        public bool EmailConfirmed { get; set; } = true;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? UserId {get; set; }
    }
}
