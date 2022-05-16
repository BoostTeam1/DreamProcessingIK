using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class PersonelDocuments : BaseEntity
    {
        [Required]
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string FileName { get; set; }
        public string FileDetails { get; set; }
        public DateTime FileGeneratedDate { get; set; }
    }
}
