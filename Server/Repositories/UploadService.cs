using FSDOCS.Server.Entities;
using FSDOCS.Server.Repositories.Contracts;
using FSDOCS.Server.Data;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;

namespace FSDOCS.Server.Repositories
{
    public class UploadService : IUploadService
    {
        private readonly FsdocsDbContext context;

        public UploadService(FsdocsDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<FailedtoUpload>> FailedtoUploadPreinsByDate(DateTime dateupload)
#pragma warning disable CS8629 // Le type valeur Nullable peut avoir une valeur null.
            => await context.FailedtoUploads
                            .Include(c => c.AnneeAcademique)
                            .Include(c => c.Etape)
                            .Where(c=>c.uploadedDate.Value.Date == dateupload.Date)
                            .OrderByDescending(c => c.uploadedDate)
                            .ToListAsync();
#pragma warning restore CS8629 // Le type valeur Nullable peut avoir une valeur null.
        public async Task<IEnumerable<FailedtoUpload>> FailedtoUploadPreins() 
            => await context.FailedtoUploads
                            .Include(c=>c.AnneeAcademique)
                            .Include(c=>c.Etape)
                            .OrderByDescending(c=>c.uploadedDate)
                            .ToListAsync();

        public async Task UploadPreinscriptions(IFormFile file)
        {
            var preins = new PreInscription();
            //var rejet = new List<PreInscription>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    //fourth sheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Preinscriptions"];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 3; row <= rowcount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value != null)
                        {
                            bool exists = context.PreInscriptions.Any(p => p.CodePreinscription==worksheet.Cells[row, 1].GetCellValue<int>());

                            if (!exists)
                            {
                                try
                                {
                                    var matricule = getMatricule(worksheet.Cells[row, 2].GetCellValue<string>(), worksheet.Cells[row, 3].GetCellValue<string>());
                                    if (!string.IsNullOrEmpty(matricule))
                                    {
                                        if (EtuExists(matricule) && EtapeExists(worksheet.Cells[row, 4].GetCellValue<string>()) && AnneeExists(worksheet.Cells[row, 5].GetCellValue<string>()))
                                        {
                                            preins.CodePreinscription = worksheet.Cells[row, 1].GetCellValue<int>();
                                            preins.Matricule = matricule;
                                            preins.CodeEtape = worksheet.Cells[row, 4].GetCellValue<string>();
                                            preins.CodeAA = worksheet.Cells[row, 5].GetCellValue<string>();

                                            await context.AddAsync(preins);
                                        }
                                    }
                                    else
                                    {
                                        if (!context.FailedtoUploads.Any(f=>f.CodePreinscription== worksheet.Cells[row, 1].GetCellValue<int>()))
                                        {
                                            if (EtapeExists(worksheet.Cells[row, 4].GetCellValue<string>()) && AnneeExists(worksheet.Cells[row, 5].GetCellValue<string>()))
                                            {
                                                //keep  inserted peinscriptions which failed here
                                                context.FailedtoUploads.Add(new FailedtoUpload
                                                {
                                                    CodePreinscription = worksheet.Cells[row, 1].GetCellValue<int>(),
                                                    Noms = worksheet.Cells[row, 2].GetCellValue<string>(),
                                                    Prenoms = worksheet.Cells[row, 3].GetCellValue<string>(),
                                                    CodeEtape = worksheet.Cells[row, 4].GetCellValue<string>(),
                                                    CodeAA = worksheet.Cells[row, 5].GetCellValue<string>(),
                                                    uploadedDate = DateTime.Now
                                                });
                                            }
                                        }
                                        
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                                
                            }
                        }
                        preins = new PreInscription();
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task UploadAA(IFormFile file)
        {
            var aa = new AnneeAcademique();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    //third sheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["AnneeAcademique"];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 3; row <= rowcount; row++)
                    {
                        
                        if (worksheet.Cells[row, 1].Value != null)
                        {
                            bool exists = context.AnneeAcademiques.Any(p => p.CodeAA.Equals(worksheet.Cells[row, 1].GetCellValue<string>()));

                            if (!exists)
                            {

#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.
                                aa.CodeAA = worksheet.Cells[row, 1].Value.ToString();
                                aa.Intitule = worksheet.Cells[row, 2].Value.ToString();
#pragma warning restore CS8601 // Existence possible d'une assignation de référence null.

                                await context.AddAsync(aa);
                            }
                        }
                        aa = new AnneeAcademique();
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task UploadDossiers(IFormFile file)
        {
            var dossier = new Dossier();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    //second sheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Dossiers"];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 3; row <= rowcount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value != null)
                        {
                            bool exists = context.Dossiers.Any(p => p.CodeDossier.Equals(worksheet.Cells[row, 1].GetCellValue<string>()));

                            if (!exists)
                            {
#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.
                                dossier.CodeDossier = worksheet.Cells[row, 1].Value.ToString();
                                dossier.Intitule = worksheet.Cells[row, 2].Value.ToString();
#pragma warning restore CS8601 // Existence possible d'une assignation de référence null.

                                await context.AddAsync(dossier);
                            }
                        }
                        dossier = new Dossier();
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task UploadEtapes(IFormFile file)
        {
            var etape = new Etape();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    //second sheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Etapes"];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 3; row <= rowcount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value != null)
                        {
                            bool exists = context.Etapes.Any(p => p.CodeEtape.Equals(worksheet.Cells[row, 1].GetCellValue<string>()));

                            if (!exists)
                            {
#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.
                                etape.CodeEtape = worksheet.Cells[row, 1].Value.ToString();
                                etape.Intitule = worksheet.Cells[row, 2].Value.ToString();
#pragma warning restore CS8601 // Existence possible d'une assignation de référence null.

                                await context.AddAsync(etape);
                            }
                        }
                        etape = new Etape();
                    }
                }
                await context.SaveChangesAsync();
            }
        }


        public async Task UploadEtudiants(IFormFile file)
        {
            var etu = new Etudiant();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    //first sheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Etudiants"];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 3; row <= rowcount; row++)
                    {
                        if (worksheet.Cells[row, 1].Value!=null)
                        {
                            bool exists = context.Etudiants.Any(p => p.Matricule.Equals(worksheet.Cells[row, 1].GetCellValue<string>()));

                            if (!exists)
                            {
#pragma warning disable CS8601 // Existence possible d'une assignation de référence null.
                                etu.Matricule = worksheet.Cells[row, 1].Value.ToString();
                                etu.Noms = worksheet.Cells[row, 2].Value.ToString();
                                etu.Prenoms = worksheet.Cells[row, 3].Value.ToString();
#pragma warning restore CS8601 // Existence possible d'une assignation de référence null.

                                await context.AddAsync(etu);
                            }
                        }
                        etu = new Etudiant();
                    }
                }
                 
                await context.SaveChangesAsync();
            }
        }

        private bool EtuExists(string matricule) => context.Etudiants.Any(c => c.Matricule.Equals(matricule));
        
        private bool EtapeExists(string codeetape) => context.Etapes.Any(c => c.CodeEtape.Equals(codeetape));
        
        private bool AnneeExists(string codeannee) => context.AnneeAcademiques.Any(c => c.CodeAA.Equals(codeannee));
        
        private string getMatricule(string name, string surname)
        {
            try
            {
                var found =  context.Etudiants.Where(e => e.Noms.Equals(name)
                                                        && e.Prenoms.Equals(surname))
                                                        .FirstOrDefault();
                                                        
                return found != null ? found.Matricule : "";
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
