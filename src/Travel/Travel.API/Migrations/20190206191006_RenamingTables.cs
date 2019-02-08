using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class RenamingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Traveler",
                table: "Traveler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Refuel",
                table: "Refuel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charge",
                table: "Charge");

            migrationBuilder.RenameTable(
                name: "Traveler",
                newName: "Travelers");

            migrationBuilder.RenameTable(
                name: "Refuel",
                newName: "Refuels");

            migrationBuilder.RenameTable(
                name: "Charge",
                newName: "Charges");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 578, DateTimeKind.Local).AddTicks(6095),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 694, DateTimeKind.Local).AddTicks(4409));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 602, DateTimeKind.Local).AddTicks(1829),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 724, DateTimeKind.Local).AddTicks(498));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Charges",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 596, DateTimeKind.Local).AddTicks(1243),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 717, DateTimeKind.Local).AddTicks(8550));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Travelers",
                table: "Travelers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Refuels",
                table: "Refuels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Travelers",
                table: "Travelers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Refuels",
                table: "Refuels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.RenameTable(
                name: "Travelers",
                newName: "Traveler");

            migrationBuilder.RenameTable(
                name: "Refuels",
                newName: "Refuel");

            migrationBuilder.RenameTable(
                name: "Charges",
                newName: "Charge");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 694, DateTimeKind.Local).AddTicks(4409),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 578, DateTimeKind.Local).AddTicks(6095));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuel",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 724, DateTimeKind.Local).AddTicks(498),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 602, DateTimeKind.Local).AddTicks(1829));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Charge",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 4, 18, 43, 14, 717, DateTimeKind.Local).AddTicks(8550),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 596, DateTimeKind.Local).AddTicks(1243));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traveler",
                table: "Traveler",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Refuel",
                table: "Refuel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charge",
                table: "Charge",
                column: "Id");
        }
    }
}
