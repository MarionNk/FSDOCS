using FSDOCS.Server.Data;
using FSDOCS.Server.Entities;
using FSDOCS.Server.Helpers;
using FSDOCS.Server.Models;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Role = FSDOCS.Server.Entities.Role;

namespace FSDOCS.Server.Repositories
{
    public class UserService : IUserService
    {
        private readonly FsdocsDbContext context;

        public UserService(FsdocsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetUsers2()
        {
           return await context.Users.Include(x => x.Groupe).Where(u => u.State).ToListAsync();
            
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
           var users = await context.Users.Include(x => x.Groupe).Where(u => u.State).ToListAsync();
            foreach (var item in users)
            {
                item.UserPwd = StringCipher.Decrypt(item.UserPwd);
            }
            return users.ToList();
        }

        public async Task<IEnumerable<Role>> UserRoles(string userid)
        {
            var user = context.Users.Where(x => x.UserID.Equals(userid)).First();
            var roles = new List<Role>();
            if (user != null)
            {
                var r = context.GroupeHasRoles.Include(x=>x.Role).Where(x => x.CodeGroupe == user.CodeGroupe).ToList();
                foreach (var item in r)
                {
                    roles.Add(item.Role);
                }
            }
            return roles;
        }

        public async Task<User> GetUser(string userid, string password)
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
         =>await context.Users.Include(x=>x.Groupe).FirstOrDefaultAsync(u => u.State && u.UserID == userid && u.UserPwd == password);

#pragma warning restore CS8603 // Existence possible d'un retour de référence null.

        public async Task<bool> CreateUser(string userid, string password)
        {
            if (context.Users.Any(x=>x.UserID.Equals(userid)))
            {
                return false;
            }
            try
            {
                var encpwd = StringCipher.HashPwd(password);
                await context.Users.AddAsync(new User { UserID=userid, UserPwd=encpwd, State=true });
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<bool> DeleteUser(string userid)
        {
            if (!context.Users.Any(x => x.UserID.Equals(userid)))
            {
                return false;
            }
            try
            {
                var user = context.Users.Where(x => x.UserID.Equals(userid)).First();
                user.State = false;
                context.Users.Update(user);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
