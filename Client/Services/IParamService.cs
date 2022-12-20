namespace FSDOCS.Client.Services
{
    public interface IParamService
    {
        Task<string> GetAccessPath();
        Task<bool> CreateAP(string ap);
    }
}
