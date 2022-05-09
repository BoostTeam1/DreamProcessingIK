using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class UserEditDto
    {
        [Required(ErrorMessage = "Kullanıcı İsmi Gerekli")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "İsim Gerekli")]
        [Display(Name = "İsim")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Soyisim Gerekli")]
        [Display(Name = "Soyisim")]
        public string LastName { get; set; }
        [Display(Name = "Tel No")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Mail Gerekli")]
        [Display(Name = "Email Adres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Gerekli")]
        [Display(Name = "Şifre Gerekli")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Doğum Yeri Gerekli")]
        [Display(Name = "Doğum Yeri")]
        public string BirthPlace { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }
    }
}
