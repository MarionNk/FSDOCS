using FSDOCS.Server.Entities;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace FSDOCS.Server.Repositories
{
    public class PreinscriptionService : IPreinscriptionService
    {
        private readonly FsdocsDbContext _context;

        public PreinscriptionService(FsdocsDbContext context)
        {
            _context = context;
        }
        public async Task<PreInscription> GetPreInscriptionsByID(int idpreins)
        {
#pragma warning disable CS8603 // Existence possible d'un retour de référence null.
            return await _context.PreInscriptions
                                 .Where(a => a.CodePreinscription == idpreins)
                                 .Include(e => e.Etudiant)
                                 .Include(e => e.Etape)
                                 .Include(c => c.Cycle)
                                 .Include(a => a.AnneeAcademique)
                                 .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Existence possible d'un retour de référence null.
        }
        public async Task<IEnumerable<PreInscription>> FilterListePreins(string? matricule, string? codeetape, string? codecycle, string? codeaa, string? etuname, string? etusurname)
        {
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
            return await _context.PreInscriptions.Where(p => (p.Matricule == matricule || matricule == null)
                                                                && (p.CodeAA == codeaa || codeaa == null)
                                                                && (p.CodeCycle == codecycle || codecycle == null)
                                                                && (p.Etudiant.Noms.Contains(etuname) || etuname == null)
                                                                && (p.Etudiant.Prenoms.Contains(etusurname)  || etusurname == null)
                                                                && (p.CodeEtape == codeetape || codeetape == null))
                                                                .Include(e => e.Etudiant)
                                                                .Include(e => e.Etape)
                                                                .Include(c => c.Cycle)
                                                                .Include(a => a.AnneeAcademique)
                                                                .ToListAsync();
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.

        }

        public async Task<IEnumerable<PreInscription>> GetPreInscriptions()
        {
            var items = await _context.PreInscriptions
                                      .Where(x=>x.CodePreinscription>0)
                                      .Include(e=>e.Etudiant)
                                      .Include(e=>e.Etape)
                                      .Include(c=>c.Cycle)
                                      .Include(a=>a.AnneeAcademique)
                                      .ToListAsync();
            return items;
        }
    }
}
