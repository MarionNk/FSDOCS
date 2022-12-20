using FSDOCS.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Components.Forms;

namespace FSDOCS.Client.Services
{
    public interface IUploadService
    {
        Task<bool> UploadAll(MultipartFormDataContent file);
        Task<IEnumerable<UploadFailedGet>> GetFUbyDate();
    }
}
