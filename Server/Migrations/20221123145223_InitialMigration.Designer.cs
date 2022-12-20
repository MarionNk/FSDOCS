﻿// <auto-generated />
using System;
using FSDOCS.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FSDOCS.Server.Migrations
{
    [DbContext(typeof(FsdocsDbContext))]
    [Migration("20221123145223_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FSDOCS.Server.Entities.AnneeAcademique", b =>
                {
                    b.Property<string>("CodeAA")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Intitule")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CodeAA");

                    b.ToTable("AnneeAcademiques");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.Cycle", b =>
                {
                    b.Property<string>("CodeCycle")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Intitule")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CodeCycle");

                    b.ToTable("Cycles");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.Dossier", b =>
                {
                    b.Property<string>("CodeDossier")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Intitule")
                        .HasColumnType("longtext");

                    b.HasKey("CodeDossier");

                    b.ToTable("Dossiers");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.Etape", b =>
                {
                    b.Property<string>("CodeEtape")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Intitule")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CodeEtape");

                    b.ToTable("Etapes");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.Etudiant", b =>
                {
                    b.Property<string>("Matricule")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Noms")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prenoms")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Matricule");

                    b.ToTable("Etudiants");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.FailedtoUpload", b =>
                {
                    b.Property<int>("CodePreinscription")
                        .HasColumnType("int");

                    b.Property<string>("CodeAA")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CodeEtape")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Noms")
                        .HasColumnType("longtext");

                    b.Property<string>("Prenoms")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("uploadedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CodePreinscription");

                    b.HasIndex("CodeAA");

                    b.HasIndex("CodeEtape");

                    b.ToTable("FailedtoUploads");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.Parametrage", b =>
                {
                    b.Property<string>("chemindacces")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("updatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("chemindacces");

                    b.ToTable("Parametrages");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.PreInscription", b =>
                {
                    b.Property<int>("CodePreinscription")
                        .HasColumnType("int");

                    b.Property<string>("CodeAA")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CodeCycle")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CodeEtape")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Matricule")
                        .HasColumnType("varchar(255)");

                    b.HasKey("CodePreinscription");

                    b.HasIndex("CodeAA");

                    b.HasIndex("CodeCycle");

                    b.HasIndex("CodeEtape");

                    b.HasIndex("Matricule");

                    b.ToTable("PreInscriptions");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("State")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserPwd")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.FailedtoUpload", b =>
                {
                    b.HasOne("FSDOCS.Server.Entities.AnneeAcademique", "AnneeAcademique")
                        .WithMany()
                        .HasForeignKey("CodeAA");

                    b.HasOne("FSDOCS.Server.Entities.Etape", "Etape")
                        .WithMany()
                        .HasForeignKey("CodeEtape");

                    b.Navigation("AnneeAcademique");

                    b.Navigation("Etape");
                });

            modelBuilder.Entity("FSDOCS.Server.Entities.PreInscription", b =>
                {
                    b.HasOne("FSDOCS.Server.Entities.AnneeAcademique", "AnneeAcademique")
                        .WithMany()
                        .HasForeignKey("CodeAA");

                    b.HasOne("FSDOCS.Server.Entities.Cycle", "Cycle")
                        .WithMany()
                        .HasForeignKey("CodeCycle");

                    b.HasOne("FSDOCS.Server.Entities.Etape", "Etape")
                        .WithMany()
                        .HasForeignKey("CodeEtape");

                    b.HasOne("FSDOCS.Server.Entities.Etudiant", "Etudiant")
                        .WithMany()
                        .HasForeignKey("Matricule");

                    b.Navigation("AnneeAcademique");

                    b.Navigation("Cycle");

                    b.Navigation("Etape");

                    b.Navigation("Etudiant");
                });
#pragma warning restore 612, 618
        }
    }
}
