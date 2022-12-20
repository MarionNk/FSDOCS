using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSDOCS.Server.Migrations
{
    public partial class codeAAParametrage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codeAA",
                table: "Parametrages",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Parametrages_codeAA",
                table: "Parametrages",
                column: "codeAA");

            migrationBuilder.AddForeignKey(
                name: "FK_Parametrages_AnneeAcademiques_codeAA",
                table: "Parametrages",
                column: "codeAA",
                principalTable: "AnneeAcademiques",
                principalColumn: "CodeAA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parametrages_AnneeAcademiques_codeAA",
                table: "Parametrages");

            migrationBuilder.DropIndex(
                name: "IX_Parametrages_codeAA",
                table: "Parametrages");

            migrationBuilder.DropColumn(
                name: "codeAA",
                table: "Parametrages");
        }
    }
}
