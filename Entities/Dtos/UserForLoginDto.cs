using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string EMail { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public Company Company { get; set; }
        public UserCompanyDto UserCompanyDto { get; set; }
    }
}
