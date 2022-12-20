using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSDOCS.Server.Migrations
{
    public partial class MigrationGroupeRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeGroupe",
                table: "Users",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fonctionnalites",
                columns: table => new
                {
                    CodeFonctionnalite = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intitule = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fonctionnalites", x => x.CodeFonctionnalite);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    CodeRole = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intitule = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.CodeRole);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Groupes",
                columns: table => new
                {
                    CodeGroupe = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Intitule = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeRole = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupes", x => x.CodeGroupe);
                    table.ForeignKey(
                        name: "FK_Groupes_Roles_CodeRole",
                        column: x => x.CodeRole,
                        principalTable: "Roles",
                        principalColumn: "CodeRole");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleHasFonctionnalites",
                columns: table => new
                {
                    CodeFonctionnalite = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeRole = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleHasFonctionnalites", x => new { x.CodeRole, x.CodeFonctionnalite });
                    table.ForeignKey(
                        name: "FK_RoleHasFonctionnalites_Fonctionnalites_CodeFonctionnalite",
                        column: x => x.CodeFonctionnalite,
                        principalTable: "Fonctionnalites",
                        principalColumn: "CodeFonctionnalite");
                    table.ForeignKey(
                        name: "FK_RoleHasFonctionnalites_Roles_CodeRole",
                        column: x => x.CodeRole,
                        principalTable: "Roles",
                        principalColumn: "CodeRole");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CodeGroupe",
                table: "Users",
                column: "CodeGroupe");

            migrationBuilder.CreateIndex(
                name: "IX_Groupes_CodeRole",
                table: "Groupes",
                column: "CodeRole");

            migrationBuilder.CreateIndex(
                name: "IX_RoleHasFonctionnalites_CodeFonctionnalite",
                table: "RoleHasFonctionnalites",
                column: "CodeFonctionnalite");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groupes_CodeGroupe",
                table: "Users",
                column: "CodeGroupe",
                principalTable: "Groupes",
                principalColumn: "CodeGroupe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groupes_CodeGroupe",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Groupes");

            migrationBuilder.DropTable(
                name: "RoleHasFonctionnalites");

            migrationBuilder.DropTable(
                name: "Fonctionnalites");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_CodeGroupe",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CodeGroupe",
                table: "Users");
        }
    }
}
