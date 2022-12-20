using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Cycle
    {
        [Key]
        public string CodeCycle { get; set; }
        public string Intitule { get; set; }
    }
}
