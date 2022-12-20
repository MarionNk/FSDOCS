using FSDOCS.Server.Entities;
using FSDOCS.Server.Models;
using FSDOCS.Shared.DataTransferObjects;
using Role = FSDOCS.Server.Entities.Role;

namespace FSDOCS.Server.Repositories.Contracts
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetUser(string userid,string password);
        public Task<bool> CreateUser(string userid,string password);
        public Task<bool> DeleteUser(string userid);
        public Task<IEnumerable<Role>> UserRoles(string userid);
    }
}
