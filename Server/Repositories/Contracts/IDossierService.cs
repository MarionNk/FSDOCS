using FSDOCS.Server.Entities;
using FSDOCS.Server.Models;

namespace FSDOCS.Server.Repositories.Contracts
{
    public interface IDossierService
    {
        public Task setAccessPath(string path);
        public Task<string> GetAccessPath();
        public Task<IEnumerable<Dossier>> getDossier();
        public Task<IEnumerable<StudentFiles>> getStudentFiles(int idpreins);

    }
}
