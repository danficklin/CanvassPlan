using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CanvassPlan.Server.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canvassers",
                columns: table => new
                {
                    CanvasserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDriver = table.Column<bool>(type: "bit", nullable: false),
                    IsLeader = table.Column<bool>(type: "bit", nullable: false),
                    IsTraining = table.Column<bool>(type: "bit", nullable: false),
                    IsAbsent = table.Column<bool>(type: "bit", nullable: false),
                    DroveYesterday = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canvassers", x => x.CanvasserId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seatbelts = table.Column<int>(type: "int", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drop = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "CanvasserCanvasser",
                columns: table => new
                {
                    DoNotPairCanvasserId = table.Column<int>(type: "int", nullable: false),
                    DoPairCanvasserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanvasserCanvasser", x => new { x.DoNotPairCanvasserId, x.DoPairCanvasserId });
                    table.ForeignKey(
                        name: "FK_CanvasserCanvasser_Canvassers_DoNotPairCanvasserId",
                        column: x => x.DoNotPairCanvasserId,
                        principalTable: "Canvassers",
                        principalColumn: "CanvasserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanvasserCanvasser_Canvassers_DoPairCanvasserId",
                        column: x => x.DoPairCanvasserId,
                        principalTable: "Canvassers",
                        principalColumn: "CanvasserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CanvasserCar",
                columns: table => new
                {
                    CarsCarId = table.Column<int>(type: "int", nullable: false),
                    DriversCanvasserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanvasserCar", x => new { x.CarsCarId, x.DriversCanvasserId });
                    table.ForeignKey(
                        name: "FK_CanvasserCar_Canvassers_DriversCanvasserId",
                        column: x => x.DriversCanvasserId,
                        principalTable: "Canvassers",
                        principalColumn: "CanvasserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanvasserCar_Cars_CarsCarId",
                        column: x => x.CarsCarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CanvasserSite",
                columns: table => new
                {
                    CanvassersCanvasserId = table.Column<int>(type: "int", nullable: false),
                    SitesSiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanvasserSite", x => new { x.CanvassersCanvasserId, x.SitesSiteId });
                    table.ForeignKey(
                        name: "FK_CanvasserSite_Canvassers_CanvassersCanvasserId",
                        column: x => x.CanvassersCanvasserId,
                        principalTable: "Canvassers",
                        principalColumn: "CanvasserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanvasserSite_Sites_SitesSiteId",
                        column: x => x.SitesSiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CanvasserTeam",
                columns: table => new
                {
                    CanvassersCanvasserId = table.Column<int>(type: "int", nullable: false),
                    TeamsTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanvasserTeam", x => new { x.CanvassersCanvasserId, x.TeamsTeamId });
                    table.ForeignKey(
                        name: "FK_CanvasserTeam_Canvassers_CanvassersCanvasserId",
                        column: x => x.CanvassersCanvasserId,
                        principalTable: "Canvassers",
                        principalColumn: "CanvasserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CanvasserTeam_Teams_TeamsTeamId",
                        column: x => x.TeamsTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarTeam",
                columns: table => new
                {
                    CarsCarId = table.Column<int>(type: "int", nullable: false),
                    TeamsTeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTeam", x => new { x.CarsCarId, x.TeamsTeamId });
                    table.ForeignKey(
                        name: "FK_CarTeam_Cars_CarsCarId",
                        column: x => x.CarsCarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarTeam_Teams_TeamsTeamId",
                        column: x => x.TeamsTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanvasserCanvasser_DoPairCanvasserId",
                table: "CanvasserCanvasser",
                column: "DoPairCanvasserId");

            migrationBuilder.CreateIndex(
                name: "IX_CanvasserCar_DriversCanvasserId",
                table: "CanvasserCar",
                column: "DriversCanvasserId");

            migrationBuilder.CreateIndex(
                name: "IX_CanvasserSite_SitesSiteId",
                table: "CanvasserSite",
                column: "SitesSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_CanvasserTeam_TeamsTeamId",
                table: "CanvasserTeam",
                column: "TeamsTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_CarTeam_TeamsTeamId",
                table: "CarTeam",
                column: "TeamsTeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanvasserCanvasser");

            migrationBuilder.DropTable(
                name: "CanvasserCar");

            migrationBuilder.DropTable(
                name: "CanvasserSite");

            migrationBuilder.DropTable(
                name: "CanvasserTeam");

            migrationBuilder.DropTable(
                name: "CarTeam");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Canvassers");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
