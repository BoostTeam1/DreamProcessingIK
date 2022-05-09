using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserNotConfirmedDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsConfirmed { get; set; }
        public bool Exist { get; set; }
        
    }
}
