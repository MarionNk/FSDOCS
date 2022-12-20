using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSDOCS.Server.Entities
{
    public class PreInscription
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CodePreinscription { get; set; }
        public string? Matricule { get; set; }
        public string? CodeEtape { get; set; }
        public string? CodeCycle { get; set; }
        public string? CodeAA { get; set; }


        public Etudiant? Etudiant { get; set; } 
        public Etape? Etape { get; set; }
        public Cycle? Cycle { get; set; } 
        public AnneeAcademique? AnneeAcademique { get; set; }
    }
}
