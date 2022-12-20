using FSDOCS.Shared.DataTransferObjects;
namespace FSDOCS.Client.Services
{
    public interface IAuthenticateService
    {
        Task<bool> authenticate(AuthenticateRequest authenticateRequest);
    }
}
