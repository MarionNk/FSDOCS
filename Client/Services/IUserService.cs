using FSDOCS.Shared.DataTransferObjects;

namespace FSDOCS.Client.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserGet>> GetUsers();
        Task<bool> UpdateUser(string userid, UpdateUser updateUser);
        Task<bool> CreateUser(AuthenticateRequest creat);
        Task<bool> CreateGroup(GroupeCreate create);
        Task<bool> DeleteUser(string userid);
        Task<IEnumerable<Role>> GetRoles();
        Task<bool> DeleteGroupe(string codegroupe);

        Task<IEnumerable<GroupeCreate>> GetGroupe();
        Task<IEnumerable<GroupeRole>> GetGroupesRole();
        Task<bool> UpdateGroupRole(string codegroupe, List<string> rolesCode);
    }
}
