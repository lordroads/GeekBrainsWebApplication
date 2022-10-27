using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeService.Database.Data.Migrations
{
    public partial class Added_Accounts_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "Accounts",
                newName: "PasswordHash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Accounts",
                newName: "passwordHash");
        }
    }
}
