using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ChangePasswordDto
    {
        [Display(Name = "Eski Şifreniz")]
        [Required]
        public string PasswordOld { get; set; }

        [Display(Name = "Yeni Şifreniz")]
        [Required]
        public string PasswordNew { get; set; }

        [Display(Name = "Yeni Şifrenizi Doğrulayın")]
        [Required]
        public string PasswordConfirm { get; set; }

    }
}
