using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDOCS.Shared.DataTransferObjects
{
    public class UploadFailedGet
    {
        public int CodePreinscription { get; set; }
        public string? Noms { get; set; }
        public string? Prenoms { get; set; }
        public string? CodeEtape { get; set; }
        public string? CodeAA { get; set; }
        public DateTime? uploadedDate { get; set; }


        public EtapeGet? Etape { get; set; }
        public AAGet? AnneeAcademique { get; set; }
    }
}
