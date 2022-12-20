using FSDOCS.Server.Entities;

namespace FSDOCS.Server.Repositories.Contracts
{
    public interface IUploadService
    {
        public Task UploadEtudiants(IFormFile file);
        public Task UploadAA(IFormFile file);
        public Task UploadEtapes(IFormFile file);
        public Task UploadPreinscriptions(IFormFile file);
        public Task UploadDossiers(IFormFile file);
        public Task<IEnumerable<FailedtoUpload>> FailedtoUploadPreins();
        public Task<IEnumerable<FailedtoUpload>> FailedtoUploadPreinsByDate(DateTime dateupload);
    }
}
