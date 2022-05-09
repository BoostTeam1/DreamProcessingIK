using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        
        public bool IsConfirmed { get; set; } = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Comment { get; set; }
        public bool IsManagerActive { get; set; }
    }
}
