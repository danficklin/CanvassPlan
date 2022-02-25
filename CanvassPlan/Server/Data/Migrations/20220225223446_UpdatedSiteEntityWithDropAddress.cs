using Microsoft.EntityFrameworkCore.Migrations;

namespace CanvassPlan.Server.Data.Migrations
{
    public partial class UpdatedSiteEntityWithDropAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "Sites",
                newName: "DropDistance");

            migrationBuilder.AddColumn<string>(
                name: "DropAddress",
                table: "Sites",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropAddress",
                table: "Sites");

            migrationBuilder.RenameColumn(
                name: "DropDistance",
                table: "Sites",
                newName: "Distance");
        }
    }
}
