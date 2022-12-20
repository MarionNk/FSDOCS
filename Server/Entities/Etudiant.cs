using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Server.Entities
{
    public class Etudiant
    {
        [Key]
        public string Matricule { get; set; }
        public string Noms { get; set; }
        public string Prenoms { get; set; }
    }
}
