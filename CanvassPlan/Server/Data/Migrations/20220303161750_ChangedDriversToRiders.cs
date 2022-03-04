using Microsoft.EntityFrameworkCore.Migrations;

namespace CanvassPlan.Server.Data.Migrations
{
    public partial class ChangedDriversToRiders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CanvasserCar_Canvassers_DriversCanvasserId",
                table: "CanvasserCar");

            migrationBuilder.RenameColumn(
                name: "DriversCanvasserId",
                table: "CanvasserCar",
                newName: "RidersCanvasserId");

            migrationBuilder.RenameIndex(
                name: "IX_CanvasserCar_DriversCanvasserId",
                table: "CanvasserCar",
                newName: "IX_CanvasserCar_RidersCanvasserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CanvasserCar_Canvassers_RidersCanvasserId",
                table: "CanvasserCar",
                column: "RidersCanvasserId",
                principalTable: "Canvassers",
                principalColumn: "CanvasserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CanvasserCar_Canvassers_RidersCanvasserId",
                table: "CanvasserCar");

            migrationBuilder.RenameColumn(
                name: "RidersCanvasserId",
                table: "CanvasserCar",
                newName: "DriversCanvasserId");

            migrationBuilder.RenameIndex(
                name: "IX_CanvasserCar_RidersCanvasserId",
                table: "CanvasserCar",
                newName: "IX_CanvasserCar_DriversCanvasserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CanvasserCar_Canvassers_DriversCanvasserId",
                table: "CanvasserCar",
                column: "DriversCanvasserId",
                principalTable: "Canvassers",
                principalColumn: "CanvasserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
