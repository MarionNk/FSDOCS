using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Parametrage
    {
        [Key]
        public string chemindacces { get; set; }
        public DateTime? updatedDate { get; set; }
        public string? codeAA { get; set; }

        public AnneeAcademique AnneeAcademique { get; set; }
    }
}
