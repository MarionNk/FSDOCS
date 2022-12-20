using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Fonctionnalite
    {
        [Key,Required]
        public string CodeFonctionnalite { get; set; }
        public string? Intitule { get; set; }
    }
}
