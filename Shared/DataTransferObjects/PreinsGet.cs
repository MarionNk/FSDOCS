
namespace FSDOCS.Shared.DataTransferObjects
{
    public class PreinsGet
    {
        public int CodePreinscription { get; set; }
        public string? Matricule { get; set; }
        public string? CodeEtape { get; set; }
        public string? CodeCycle { get; set; }
        public string? CodeAA { get; set; }


        public EtudiantGet? Etudiant { get; set; }
        public EtapeGet? Etape { get; set; }
        public CycleGet? Cycle { get; set; }
        public AAGet? AnneeAcademique { get; set; }
        
    }
}
