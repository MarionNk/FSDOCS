using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Etape
    {
        [Key]
        public string CodeEtape { get; set; }
        public string Intitule { get; set; }
    }
}
