using Microsoft.EntityFrameworkCore.Migrations;

namespace CanvassPlan.Server.Data.Migrations
{
    public partial class AnotherFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Canvassers",
                newName: "Inactive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Inactive",
                table: "Canvassers",
                newName: "IsActive");
        }
    }
}
