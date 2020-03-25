using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddedPhoneNumberToAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Accounts",
                type: "nvarchar(12)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Accounts");
        }
    }
}
