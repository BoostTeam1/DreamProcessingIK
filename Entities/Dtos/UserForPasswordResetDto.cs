using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
public class UserForPasswordResetDto
    {
        [Required]
        public string Email { get; set; }
        [Display(Name ="Yeni Şifreniz")]
        [Required]
        public string PasswordNew { get; set; }
 
    }
}
