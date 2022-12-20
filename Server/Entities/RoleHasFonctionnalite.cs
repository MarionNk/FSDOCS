using OfficeOpenXml.Style;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSDOCS.Server.Entities
{
    public class RoleHasFonctionnalite
    {
        public string CodeFonctionnalite { get; set; }
        public string CodeRole { get; set; }

        public Fonctionnalite Fonctionnalites { get; set; }
        public Role Role { get; set; }
    }
}
