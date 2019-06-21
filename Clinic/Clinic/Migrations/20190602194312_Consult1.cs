using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Migrations
{
    public partial class Consult1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Assistants");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Consultations",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Consultations");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Assistants",
                maxLength: 100,
                nullable: true);
        }
    }
}
