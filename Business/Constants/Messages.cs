using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string UsersAdded = "Kişi başarıyla eklendi";
        public static string UsersError = "Kişi eklenemedi";

        public static string CompanyAdded = "Şirket başarıyla eklendi";
        public static string CompanyDeleted = "Şirket başarıyla silindi";
        public static string CompanyUpdated = "Şirket başarıyla güncellendi";
        public static string CompanyError = "Şirket eklenemedi";

        public static string CompanySectorAdded = "Şirket ve sektör bağlantısı başarıyla eklendi";
        public static string CompanySectorDeleted = "Şirket ve sektör bağlantısı başarıyla silindi";
        public static string CompanySectorUpdated = "Şirket ve sektör bağlantısı başarıyla güncellendi";
        public static string CompanySectorError = "Şirket ve sektör bağlantısı eklenemedi";
    }
}
