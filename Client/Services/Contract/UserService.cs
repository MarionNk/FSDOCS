using FSDOCS.Shared.DataTransferObjects;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

namespace FSDOCS.Client.Services.Contract
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpclient;

        public UserService(HttpClient httpclient)
        {
            this.httpclient = httpclient;
        }


        public async Task<bool> UpdateUser(string userid,UpdateUser updateUser)
        {
            try
            {
                var up = await this.httpclient.PutAsJsonAsync($"/Groupes/UpdateUser/{userid}", updateUser);
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateGroupRole(string codegroupe, List<string> rolesCode)
        {
            try
            {
                var up = await this.httpclient.PostAsJsonAsync($"Groupes/UpdateGroupeRole/{codegroupe}", rolesCode);
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateUser(AuthenticateRequest create)
        {
            try
            {
                var up = await this.httpclient.PostAsJsonAsync($"/Users/Create", create);
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CreateGroup(GroupeCreate create)
        {
            try
            {
                var up = await this.httpclient.PostAsJsonAsync($"/Groupes/CreateGroup", create);
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteUser(string userid)
        {
            try
            {
                var up = await this.httpclient.DeleteAsync($"/Users/Delete/{userid}");
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
         public async Task<bool> DeleteGroupe(string codegroupe)
        {
            try
            {
                var up = await this.httpclient.DeleteAsync($"/Groupes/DeleteGroupe/{codegroupe}");
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            try
            {
                var list = await this.httpclient.GetFromJsonAsync<IEnumerable<Role>>("/Groupes/GetRoles");
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<UserGet>> GetUsers()
        {
            try
            {
                var list = await this.httpclient.GetFromJsonAsync<IEnumerable<UserGet>>("/Users/GetUsers");
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<GroupeCreate>> GetGroupe()
        {
            try
            {
                var list = await this.httpclient.GetFromJsonAsync<IEnumerable<GroupeCreate>>("/Groupes/GetGroupe");
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<GroupeRole>> GetGroupesRole()
        {
            try
            {
                var list = await this.httpclient.GetFromJsonAsync<IEnumerable<GroupeRole>>("/Groupes/GetGroupesRole");
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> GetGroupByCode(string codegrp)
        {
            try
            {
                var up = await this.httpclient.GetAsync($"/Groupes/GetGBId/{codegrp}");
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
