using FSDOCS.Server.Data;
using FSDOCS.Server.Helpers;
using FSDOCS.Server.Models;
using FSDOCS.Server.Repositories.Contracts;
using System.Security.Claims;
using System.Text;
using AuthenticateResponse = FSDOCS.Shared.DataTransferObjects.AuthenticateResponse;

namespace FSDOCS.Server.Repositories
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly FsdocsDbContext _context;
        private readonly IUserService userService;
        
        public TokenService(IConfiguration configuration, FsdocsDbContext contexte, IUserService userService)
        {
            _configuration = configuration;
            _context = contexte;
            this.userService = userService;
        }
        public bool AuthenticateUser(string userid, string password)
        {
            if (!_context.Users.Any(x => x.UserID.Equals(userid) && x.UserPwd.Equals(StringCipher.HashPwd(password))))
                return false;
            
            return true;
        }
        public AuthenticateResponse NewToken(string userid, string password)
        {

            var key = _configuration.GetValue<string>("TokenConfig:key");

            var token = StringCipher.Encrypt(key);
            var roles = this.userService.UserRoles(userid);
            var group = this.userService.GetUser(userid, StringCipher.HashPwd(password)).Result.CodeGroupe;
            var listroles = new List<String>();

            foreach (var item in roles.Result)
            {
                listroles.Add(item.CodeRole);
            }

            var tokenresponse = new AuthenticateResponse { Token = token,RolesCode=listroles, Group=group, ExpiryDate = DateTime.Now.AddMinutes(5), UserPseudo = userid };

            return tokenresponse;
        }

    }
}
