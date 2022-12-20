using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class FailedtoUpload
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodePreinscription { get; set; }
        public string? Noms { get; set; }
        public string? Prenoms { get; set; }
        public string? CodeEtape { get; set; }
        public string? CodeAA { get; set; }
        public DateTime? uploadedDate { get; set; }


        public Etape? Etape { get; set; }
        public AnneeAcademique? AnneeAcademique { get; set; }
    }
}
