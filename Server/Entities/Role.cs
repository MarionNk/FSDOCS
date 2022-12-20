using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Role
    {
        [Key, Required]
        public string CodeRole { get; set; }
        public string? Intitule { get; set; }
    }
}
