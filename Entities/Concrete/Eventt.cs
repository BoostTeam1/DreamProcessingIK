using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Eventt
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string Color { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string TextColor { get; set; }
    }
}
