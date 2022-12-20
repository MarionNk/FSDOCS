using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class AnneeAcademique
    {
        [Key]
        public string CodeAA { get; set; }
        public string Intitule { get; set; }
    }
}
