using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class PersonnelDocumentsDto
    {
        public string AppUserId { get; set; }
        public IFormFile FilePath { get; set; }
        public string FileName { get; set; }
        public string FileDetails { get; set; }
        public DateTime FileGeneratedDate { get; set; }
    }
}
