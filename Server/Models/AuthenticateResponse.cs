using FSDOCS.Server.Entities;

namespace FSDOCS.Server.Models
{
    public class AuthenticateResponse
    {
        public string UserPseudo { get; set; }
        public string Token { get; set; }
        public string Group { get; set; }
        public List<string> RolesCode { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}
