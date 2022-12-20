using FSDOCS.Server.Entities;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Role = FSDOCS.Shared.DataTransferObjects.Role;

namespace FSDOCS.Server.Repositories.Contracts
{
    public interface IGroupeService
    {
        public Task<Groupe> GetGroupeByID(string codegroupe);
        public Task<IEnumerable<Groupe>> GetGroupes();
        public Task<IEnumerable<Role>> GetRoles();

        public Task<IEnumerable<GroupeHasRole>> GetRolesByGroupe(string codegroupe);
        public Task<bool> ChangeUserGroup(string userid,UpdateUser updateUser);
        public Task<IEnumerable<GroupeRole>> GetGroupesRole();

        public Task<bool> UpdateGroupeRole(string codegroupe, List<string> rolesCode);
        public Task<bool> removeRoleToGrp(string codegroupe, string coderole);
        public Task<bool> removeGrp(string codegroupe);

        public  Task CreateGroup(GroupeCreate group);


    }
}