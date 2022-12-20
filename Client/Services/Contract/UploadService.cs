using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace FSDOCS.Client.Services.Contract
{
    public class UploadService : IUploadService
    {
        private readonly HttpClient httpclient;

        public UploadService(HttpClient httpclient)
        {
            this.httpclient = httpclient;
        }


        public async Task<bool> UploadAll(MultipartFormDataContent content)
        {
            try
            {
                var up = await this.httpclient.PostAsync($"/Upload/All",content);
                if (up.IsSuccessStatusCode)
                {
                    if (up.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        Console.WriteLine("no content");
                        return false;
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("upload failed");
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<UploadFailedGet>> GetFUbyDate()
        {
            try
            {
                var date = DateTime.Now;
                var newdatestruct = date.Year + "-" + date.Month + "-" + date.Day;
                var uf = await this.httpclient.GetAsync($"/Upload/GetFUbyDate/{newdatestruct}");
                if (uf.IsSuccessStatusCode)
                {
                    if (uf.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(IEnumerable<UploadFailedGet>);
                    }
                    return await uf.Content.ReadFromJsonAsync<IEnumerable<UploadFailedGet>>();
                }
                else
                {
                    var message = await uf.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
