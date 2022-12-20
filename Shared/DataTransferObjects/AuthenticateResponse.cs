using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDOCS.Shared.DataTransferObjects
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
