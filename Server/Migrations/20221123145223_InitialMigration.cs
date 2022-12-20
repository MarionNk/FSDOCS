using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSDOCS.Server.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AnneeAcademiques",
                columns: table => new
                {
                    CodeAA = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intitule = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnneeAcademiques", x => x.CodeAA);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cycles",
                columns: table => new
                {
                    CodeCycle = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intitule = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cycles", x => x.CodeCycle);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    CodeDossier = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intitule = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.CodeDossier);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Etapes",
                columns: table => new
                {
                    CodeEtape = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intitule = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etapes", x => x.CodeEtape);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    Matricule = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Noms = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prenoms = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.Matricule);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parametrages",
                columns: table => new
                {
                    chemindacces = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parametrages", x => x.chemindacces);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserPwd = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    State = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FailedtoUploads",
                columns: table => new
                {
                    CodePreinscription = table.Column<int>(type: "int", nullable: false),
                    Noms = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prenoms = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeEtape = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeAA = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uploadedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailedtoUploads", x => x.CodePreinscription);
                    table.ForeignKey(
                        name: "FK_FailedtoUploads_AnneeAcademiques_CodeAA",
                        column: x => x.CodeAA,
                        principalTable: "AnneeAcademiques",
                        principalColumn: "CodeAA");
                    table.ForeignKey(
                        name: "FK_FailedtoUploads_Etapes_CodeEtape",
                        column: x => x.CodeEtape,
                        principalTable: "Etapes",
                        principalColumn: "CodeEtape");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PreInscriptions",
                columns: table => new
                {
                    CodePreinscription = table.Column<int>(type: "int", nullable: false),
                    Matricule = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeEtape = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeCycle = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeAA = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreInscriptions", x => x.CodePreinscription);
                    table.ForeignKey(
                        name: "FK_PreInscriptions_AnneeAcademiques_CodeAA",
                        column: x => x.CodeAA,
                        principalTable: "AnneeAcademiques",
                        principalColumn: "CodeAA");
                    table.ForeignKey(
                        name: "FK_PreInscriptions_Cycles_CodeCycle",
                        column: x => x.CodeCycle,
                        principalTable: "Cycles",
                        principalColumn: "CodeCycle");
                    table.ForeignKey(
                        name: "FK_PreInscriptions_Etapes_CodeEtape",
                        column: x => x.CodeEtape,
                        principalTable: "Etapes",
                        principalColumn: "CodeEtape");
                    table.ForeignKey(
                        name: "FK_PreInscriptions_Etudiants_Matricule",
                        column: x => x.Matricule,
                        principalTable: "Etudiants",
                        principalColumn: "Matricule");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FailedtoUploads_CodeAA",
                table: "FailedtoUploads",
                column: "CodeAA");

            migrationBuilder.CreateIndex(
                name: "IX_FailedtoUploads_CodeEtape",
                table: "FailedtoUploads",
                column: "CodeEtape");

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptions_CodeAA",
                table: "PreInscriptions",
                column: "CodeAA");

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptions_CodeCycle",
                table: "PreInscriptions",
                column: "CodeCycle");

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptions_CodeEtape",
                table: "PreInscriptions",
                column: "CodeEtape");

            migrationBuilder.CreateIndex(
                name: "IX_PreInscriptions_Matricule",
                table: "PreInscriptions",
                column: "Matricule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dossiers");

            migrationBuilder.DropTable(
                name: "FailedtoUploads");

            migrationBuilder.DropTable(
                name: "Parametrages");

            migrationBuilder.DropTable(
                name: "PreInscriptions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AnneeAcademiques");

            migrationBuilder.DropTable(
                name: "Cycles");

            migrationBuilder.DropTable(
                name: "Etapes");

            migrationBuilder.DropTable(
                name: "Etudiants");
        }
    }
}
