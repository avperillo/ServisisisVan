using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "charge_sequence",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "refuel_sequence",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "traveler_sequence",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "trip_sequence",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Charge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 717, DateTimeKind.Local).AddTicks(8550)),
                    IdTraveler = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Refuel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 724, DateTimeKind.Local).AddTicks(498)),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refuel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traveler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDriver = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traveler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 694, DateTimeKind.Local).AddTicks(4409)),
                    IdDriver = table.Column<int>(nullable: false),
                    Commentary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charge");

            migrationBuilder.DropTable(
                name: "Refuel");

            migrationBuilder.DropTable(
                name: "Traveler");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropSequence(
                name: "charge_sequence");

            migrationBuilder.DropSequence(
                name: "refuel_sequence");

            migrationBuilder.DropSequence(
                name: "traveler_sequence");

            migrationBuilder.DropSequence(
                name: "trip_sequence");
        }
    }
}
