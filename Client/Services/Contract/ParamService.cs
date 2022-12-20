namespace FSDOCS.Client.Services.Contract
{
    public class ParamService : IParamService
    {
        private readonly HttpClient httpclient;

        public ParamService(HttpClient httpclient)
        {
            this.httpclient = httpclient;
        }
        public async Task<bool> CreateAP(string ap)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("path", ap)
                });
                var up = await this.httpclient.PostAsync($"/Parametres/setAP", content);
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

        public async Task<string> GetAccessPath()
        {
            try
            {
                var path = await this.httpclient.GetStringAsync("/Parametres/getAP");
                return path;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
