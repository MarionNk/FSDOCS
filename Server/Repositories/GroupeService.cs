using FSDOCS.Server.Data;
using FSDOCS.Server.Entities;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using FSDOCS.Server.Helpers;
using static MudBlazor.CategoryTypes;
using Role = FSDOCS.Shared.DataTransferObjects.Role;

namespace FSDOCS.Server.Repositories
{
    public class GroupeService: IGroupeService
    {
        private readonly FsdocsDbContext context;

        public GroupeService(FsdocsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<GroupeRole>> GetGroupesRole()
        {
            var result = new List<GroupeRole>();
            var gr= await context.Groupes.ToListAsync();
            foreach (var item in gr)
            {
                var r = await context.GroupeHasRoles.Include(x=>x.Role).Where(x => x.CodeGroupe.Equals(item.CodeGroupe)).ToListAsync();
                var listroles = new List<RoleGet>();
                foreach (var x in r)
                {
                    listroles.Add(new RoleGet { CodeRole = x.CodeRole, Intitule = x.Role?.Intitule });
                }
                var groupebtid = GetGroupeByID(item.CodeGroupe);
                result.Add(new GroupeRole
                {
                    codegroupe = item.CodeGroupe,
                    roles = listroles,
                    groupe = new GroupeCreate { CodeGroupe = groupebtid.Result.CodeGroupe, Intitule = groupebtid.Result.Intitule }
                });
            }
            return result;
        }
        public async Task<IEnumerable<Groupe>> GetGroupes()
        {
            return await context.Groupes.ToListAsync();
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            var listrole= await context.Roles.ToListAsync();
            var listresult = new List<Role>();
            foreach (var item in listrole)
            {
                listresult.Add(new Role { CodeRole = item.CodeRole, Intitule = item.Intitule });
            }
            return listresult;
        }
        public async Task<Groupe> GetGroupeByID(string codegroupe)
        {
            return await context.Groupes.Where(x=>x.CodeGroupe==codegroupe).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GroupeHasRole>> GetRolesByGroupe(string codegroupe)
        {
            return await context.GroupeHasRoles.Include(x=>x.Groupe).Include(x=>x.Role).Where(x=>x.CodeGroupe.Equals(codegroupe)).ToListAsync();
        }
        //used to update user too
        public async Task<bool> ChangeUserGroup(string userid,UpdateUser updateUser)
        {
            var user = await context.Users.Where(x=>x.UserID==userid).FirstOrDefaultAsync();
            if (user!=null)
            {
                if (context.Groupes.Any(x => x.CodeGroupe == updateUser.CodeGroupe))
                {
                    user.CodeGroupe = updateUser.CodeGroupe;
                    if (!StringCipher.Decrypt(user.UserPwd).Equals(updateUser.UserPwd))
                    {
                        user.UserPwd = StringCipher.HashPwd(updateUser.UserPwd);
                    }
                    
                    context.Users.Update(user);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> AddRolesToGroupe(string codegroupe, List<string> rolesCode)
        {
            if (context.Groupes.Any(x=>x.CodeGroupe==codegroupe))
            {
                foreach (var item in rolesCode)
                {
                    if (context.Roles.Any(x => x.CodeRole == item))
                        await context.GroupeHasRoles.AddAsync(
                            new GroupeHasRole { CodeGroupe=codegroupe,CodeRole=item }
                        );
                }
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateGroupeRole(string codegroupe, List<string> rolesCode)
        {
            var groupe = context.GroupeHasRoles.Where(x => x.CodeGroupe == codegroupe).ToList();
            if(rolesCode.Count()>0)
            {
                //delete what was there at the first place
                foreach (var item in groupe)
                {
                    context.GroupeHasRoles.Remove(item);
                }
                //add new
                foreach (var item in rolesCode)
                {
                    if (context.Roles.Any(x => x.CodeRole == item))
                        await context.GroupeHasRoles.AddAsync(
                            new GroupeHasRole { CodeGroupe = codegroupe, CodeRole = item }
                        );
                }
                await context.SaveChangesAsync();
                return true;

            }

            return false;
        }
        public async Task<bool> UpdateGroupeRoleTODELETE(string codegroupe, List<string> rolesCode)
        {
            if (context.Groupes.Any(x=>x.CodeGroupe==codegroupe))
            {
                foreach (var item in rolesCode)
                {
                    if (context.Roles.Any(x => x.CodeRole == item))
                    { 
                        if(!context.GroupeHasRoles.Any(x=>x.CodeGroupe.Equals(codegroupe) && x.CodeRole.Equals(item)))
                        {
                            await context.GroupeHasRoles.AddAsync(
                                new GroupeHasRole { CodeGroupe = codegroupe, CodeRole = item }
                            );
                        } 
                    }
                }
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> removeRoleToGrp(string codegroupe,string coderole)
        {
            var gr = await context.GroupeHasRoles.Where(x => x.CodeGroupe.Equals(codegroupe) && x.CodeRole.Equals(coderole)).FirstOrDefaultAsync();
            if(gr!=null)
            {
                context.GroupeHasRoles.Remove(gr);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> removeGrp(string codegroupe)
        {
            var gr=await context.Groupes.Where(x=>x.CodeGroupe==codegroupe).FirstOrDefaultAsync();
            if(gr!=null)
            {
                if (context.GroupeHasRoles.Any(x => x.CodeGroupe == codegroupe))
                {
                    foreach (var item in context.GroupeHasRoles.Where(x => x.CodeGroupe == codegroupe).ToList())
                    {
                        context.GroupeHasRoles.Remove(item);

                    }
                }
                if (context.Users.Any(x => x.CodeGroupe == codegroupe))
                {
                    foreach (var item in context.Users.Where(x => x.CodeGroupe == codegroupe).ToList())
                    {
                        item.CodeGroupe = null;
                        context.Update(item);
                    }
                }

                context.Groupes.Remove(gr);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task CreateGroup(GroupeCreate group)
        {
            if (group.CodeGroupe.Length>0 && !context.Groupes.Any(x=>x.CodeGroupe==group.CodeGroupe))
            {
                await context.Groupes.AddAsync(new Groupe { CodeGroupe = group.CodeGroupe, Intitule = group.Intitule });
                await context.SaveChangesAsync();
            }
        }




    }
}
