using FSDOCS.Shared.DataTransferObjects;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;
using System.Text;
using Blazored.SessionStorage;

namespace FSDOCS.Client.Services.Contract
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly HttpClient httpclient;
        private readonly ISessionStorageService sessionStorage;

        public AuthenticateService(HttpClient httpclient, ISessionStorageService sessionStorage)
        {
            this.httpclient = httpclient;
            this.sessionStorage = sessionStorage;
        }
        public async Task<bool> authenticate(AuthenticateRequest authenticateRequest)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(authenticateRequest);
                var data = new StringContent(myContent, Encoding.UTF8, "application/json");

                var up = await this.httpclient.PostAsync($"/Authenticate/Login", data);

                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }
                    var responseJson = await up.Content.ReadFromJsonAsync<AuthenticateResponse>();
                    await sessionStorage.SetItemAsync<List<string>>("Roles", responseJson.RolesCode);
                    await sessionStorage.SetItemAsync<string>("Group", responseJson.Group);
                    await sessionStorage.SetItemAsync<string>("Userid", responseJson.UserPseudo);
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
