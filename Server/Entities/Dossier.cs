using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Dossier
    {
        [Key, Required]
        public string CodeDossier { get; set; }
        public string? Intitule { get; set; }
    }
}
