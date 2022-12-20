using FSDOCS.Server.Entities;

namespace FSDOCS.Server.Repositories.Contracts
{
    public interface IPreinscriptionService
    {
        public Task<IEnumerable<PreInscription>> GetPreInscriptions();
        public Task<PreInscription> GetPreInscriptionsByID(int idpreins);
        public Task<IEnumerable<PreInscription>> FilterListePreins(string? matricule, string? codeetape, string? codecycle, string? codeaa, string? etuname, string? etusurname);
    }
}
