using FSDOCS.Server.Models;
using AuthenticateResponse = FSDOCS.Shared.DataTransferObjects.AuthenticateResponse;

namespace FSDOCS.Server.Repositories.Contracts
{
    public interface ITokenService
    {
        public bool AuthenticateUser(string userid, string password);
        public AuthenticateResponse NewToken(string userid, string password);
    }
}
