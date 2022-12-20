using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FSDOCS.Server.Entities
{
    public class User
    {
        [Key,Required]
        public string UserID { get; set; }
        [Required]
        //[JsonIgnore] //prevents password property from being serialized and returned in api responses
        public string UserPwd { get; set; }
        public string? CodeGroupe { get; set; }

        public bool State { get; set; }

        public Groupe? Groupe { get; set; }
    }
}
