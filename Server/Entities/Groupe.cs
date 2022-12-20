using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Groupe
    {
        [Key, Required]
        public string CodeGroupe { get; set; }
        public string? Intitule { get; set; }
        
    }
}
