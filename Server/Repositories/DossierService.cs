using FSDOCS.Server.Data;
using FSDOCS.Server.Entities;
using FSDOCS.Server.Models;
using FSDOCS.Server.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace FSDOCS.Server.Repositories
{
    public class DossierService : IDossierService
    {
        private readonly FsdocsDbContext context;

        public DossierService(FsdocsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Dossier>> getDossier() => await context.Dossiers.ToListAsync();

        public async Task<IEnumerable<StudentFiles>> getStudentFiles(int idpreins)
        {
            var studentfiles = new List<StudentFiles>();
            var preins = await context.PreInscriptions.Where(p => p.CodePreinscription == idpreins).FirstOrDefaultAsync();

            if (preins!=null)
            {
                //get accessPath
                var accessPath = context.Parametrages.First(x=>x.codeAA==preins.CodeAA);

                if (Directory.Exists(accessPath.chemindacces))
                {
                    //foreach subdirectories
                    foreach (var dir in Directory.GetDirectories(accessPath.chemindacces))
                    {
                        //for each file get the corresponding idpreins
                        foreach (string file in Directory.GetFiles(dir))
                        {
                            if (Path.GetFileName(file).Split(".")[0].Equals(idpreins.ToString()))
                            {
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
                                studentfiles.Add(new StudentFiles
                                {
                                    codeDossier = await getDossierName(Path.GetDirectoryName(file).Split("\\").Last()),
                                    pathToFile = file.Split("wwwroot\\")[1]
                                });;
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("hummm");
            }
            return studentfiles;
        }
        private async Task<String?> getDossierName(String codedoss)
        { 
            var doss=await context.Dossiers.FirstOrDefaultAsync(x => x.CodeDossier == codedoss);
            return doss.Intitule;
        }
        public async Task setAccessPath(string path)
        {
            if (context.Parametrages.Count() > 0)
            {
                var param = context.Parametrages.Where(p => !string.IsNullOrEmpty(p.chemindacces)).First();
                context.Parametrages.Remove(param);
            }
            context.Parametrages.Add(new Parametrage
            {
                chemindacces = path,
                updatedDate = DateTime.Now
            });

            await context.SaveChangesAsync();
        }

        public async Task<string> GetAccessPath()
        {
            var ap = await context.Parametrages.FirstAsync();
            return ap.chemindacces;
        }
    }
}
