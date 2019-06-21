using Microsoft.EntityFrameworkCore.Migrations;

namespace Clinic.Migrations
{
    public partial class Insurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "InsuranceCompanies",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Insurance_Id",
                table: "InsuranceCompanies",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fax",
                table: "InsuranceCompanies");

            migrationBuilder.DropColumn(
                name: "Insurance_Id",
                table: "InsuranceCompanies");
        }
    }
}
