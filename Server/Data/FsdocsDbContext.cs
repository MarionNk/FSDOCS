using FSDOCS.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace FSDOCS.Server.Data
{
    public class FsdocsDbContext:DbContext
    {
        protected readonly IConfiguration Configuration;

#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public FsdocsDbContext(IConfiguration configuration)
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        {
            Configuration = configuration;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var constring = Configuration.GetConnectionString("SecuredFSDOCSSQLServerDBConnection");
            options.UseMySql(constring, ServerVersion.AutoDetect(constring));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //failed to upload
            modelBuilder.Entity<FailedtoUpload>()
                .HasOne(x => x.AnneeAcademique)
                .WithMany()
                .HasForeignKey(x => x.CodeAA)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<FailedtoUpload>()
                .HasOne(x => x.Etape)
                .WithMany()
                .HasForeignKey(x => x.CodeEtape)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //users
            modelBuilder.Entity<User>()
               .HasOne(x => x.Groupe)
               .WithMany()
               .HasForeignKey(x => x.CodeGroupe)
               .OnDelete(DeleteBehavior.ClientSetNull);
            //Parametres
            modelBuilder.Entity<Parametrage>()
                .HasOne(x => x.AnneeAcademique)
                .WithMany()
                .HasForeignKey(x => x.codeAA)
                .OnDelete(DeleteBehavior.ClientSetNull);
            //preinscriptions
            modelBuilder.Entity<PreInscription>()
                .HasOne(x => x.AnneeAcademique)
                .WithMany()
                .HasForeignKey(x => x.CodeAA)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PreInscription>()
                .HasOne(x => x.Etudiant)
                .WithMany()
                .HasForeignKey(x => x.Matricule)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PreInscription>()
                .HasOne(x => x.Etape)
                .WithMany()
                .HasForeignKey(x => x.CodeEtape)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PreInscription>()
                .HasOne(x => x.Cycle)
                .WithMany()
                .HasForeignKey(x => x.CodeCycle)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //GroupeHasRole

            modelBuilder.Entity<GroupeHasRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.CodeRole)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<GroupeHasRole>()
                .HasOne(x => x.Groupe)
                .WithMany()
                .HasForeignKey(x => x.CodeGroupe)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<GroupeHasRole>()
                .HasKey(p => new { p.CodeRole, p.CodeGroupe });

            //Rolehasfonctionnalite

            modelBuilder.Entity<RoleHasFonctionnalite>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.CodeRole)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<RoleHasFonctionnalite>()
                .HasOne(x => x.Fonctionnalites)
                .WithMany()
                .HasForeignKey(x => x.CodeFonctionnalite)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<RoleHasFonctionnalite>()
                .HasKey(p => new { p.CodeRole, p.CodeFonctionnalite });

        }
        public DbSet<Etape> Etapes { get; set; } 
        public DbSet<Etudiant> Etudiants { get; set; } 
        public DbSet<Cycle> Cycles { get; set; }
        public DbSet<AnneeAcademique> AnneeAcademiques { get; set; } 
        public DbSet<PreInscription> PreInscriptions { get; set; }
        public DbSet<FailedtoUpload> FailedtoUploads { get; set; }
        public DbSet<Parametrage> Parametrages { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Groupe> Groupes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleHasFonctionnalite> RoleHasFonctionnalites { get; set; }
        public DbSet<Fonctionnalite> Fonctionnalites { get; set; }
        public DbSet<GroupeHasRole> GroupeHasRoles { get; set; }

        
    }
}
