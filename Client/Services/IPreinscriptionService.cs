
using FSDOCS.Shared.DataTransferObjects;

namespace FSDOCS.Client.Services
{
    public interface IPreinscriptionService
    {
        Task<IEnumerable<PreinsGet>> GetItems();
        Task<IEnumerable<StudentFiles>> GetStudentFiles(int idpreins);
        Task<PreinsGet> GetPreins(int idpreins);
    }
}
