using Microsoft.EntityFrameworkCore.Migrations;

namespace CanvassPlan.Server.Data.Migrations
{
    public partial class FixedAgainMaybe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Teams",
                newName: "Inactive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Sites",
                newName: "Inactive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Cars",
                newName: "Inactive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Inactive",
                table: "Teams",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Inactive",
                table: "Sites",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Inactive",
                table: "Cars",
                newName: "IsActive");
        }
    }
}
