using FSDOCS.Shared.DataTransferObjects;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FSDOCS.Client.Services.Contract
{
    public class PreinscriptionService:IPreinscriptionService
    {
        private readonly HttpClient httpclient;

        public PreinscriptionService(HttpClient httpclient)
        {
            this.httpclient = httpclient;
            
        }
        public async Task<IEnumerable<PreinsGet>> GetItems()
        {
            try
            {
                var preins = await this.httpclient.GetFromJsonAsync<IEnumerable<PreinsGet>>("/Preins/GetList");
                return preins;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<StudentFiles>> GetStudentFiles(int idpreins)
        {
            try
            {
                var sf = await this.httpclient.GetAsync($"/Parametres/getSF/{idpreins}");
                if (sf.IsSuccessStatusCode)
                {
                    if (sf.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(IEnumerable<StudentFiles>);
                    }
                    return await sf.Content.ReadFromJsonAsync<IEnumerable<StudentFiles>>();
                }
                else
                {
                    var message = await sf.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        } 
        public async Task<PreinsGet> GetPreins(int idpreins)
        {
            try
            {
                var sf = await this.httpclient.GetAsync($"/Preins/GetByID/{idpreins}");
                if (sf.IsSuccessStatusCode)
                {
                    if (sf.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(PreinsGet);
                    }
                    return await sf.Content.ReadFromJsonAsync<PreinsGet>();
                }
                else
                {
                    var message = await sf.Content.ReadAsStringAsync();
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
