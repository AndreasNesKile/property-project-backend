using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class AddedCoordinatesToProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "XCoordinate",
                table: "Properties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YCoordinate",
                table: "Properties",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XCoordinate",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "YCoordinate",
                table: "Properties");
        }
    }
}
