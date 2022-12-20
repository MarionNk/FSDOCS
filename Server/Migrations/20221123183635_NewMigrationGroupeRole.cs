using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSDOCS.Server.Migrations
{
    public partial class NewMigrationGroupeRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groupes_Roles_CodeRole",
                table: "Groupes");

            migrationBuilder.DropIndex(
                name: "IX_Groupes_CodeRole",
                table: "Groupes");

            migrationBuilder.DropColumn(
                name: "CodeRole",
                table: "Groupes");

            migrationBuilder.CreateTable(
                name: "GroupeHasRoles",
                columns: table => new
                {
                    CodeGroupe = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodeRole = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupeHasRoles", x => new { x.CodeRole, x.CodeGroupe });
                    table.ForeignKey(
                        name: "FK_GroupeHasRoles_Groupes_CodeGroupe",
                        column: x => x.CodeGroupe,
                        principalTable: "Groupes",
                        principalColumn: "CodeGroupe");
                    table.ForeignKey(
                        name: "FK_GroupeHasRoles_Roles_CodeRole",
                        column: x => x.CodeRole,
                        principalTable: "Roles",
                        principalColumn: "CodeRole");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GroupeHasRoles_CodeGroupe",
                table: "GroupeHasRoles",
                column: "CodeGroupe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupeHasRoles");

            migrationBuilder.AddColumn<string>(
                name: "CodeRole",
                table: "Groupes",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Groupes_CodeRole",
                table: "Groupes",
                column: "CodeRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Groupes_Roles_CodeRole",
                table: "Groupes",
                column: "CodeRole",
                principalTable: "Roles",
                principalColumn: "CodeRole");
        }
    }
}
