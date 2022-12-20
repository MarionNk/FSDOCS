using System.ComponentModel.DataAnnotations;

namespace FSDOCS.Shared.DataTransferObjects
{
    public class AuthenticateRequest
    {
        [Required]
        public string   Userid { get; set; }

        [Required]
        public string Password { get; set; }
    }
}