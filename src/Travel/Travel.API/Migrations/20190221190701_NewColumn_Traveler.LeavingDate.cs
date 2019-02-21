using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class NewColumn_TravelerLeavingDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 21, 20, 7, 0, 989, DateTimeKind.Local).AddTicks(1956),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 839, DateTimeKind.Local).AddTicks(9352));

            migrationBuilder.AddColumn<DateTime>(
                name: "LeavingDate",
                table: "Travelers",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 36, DateTimeKind.Local).AddTicks(3382),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 978, DateTimeKind.Local).AddTicks(2094));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 30, DateTimeKind.Local).AddTicks(1791),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 972, DateTimeKind.Local).AddTicks(8081));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeavingDate",
                table: "Travelers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 839, DateTimeKind.Local).AddTicks(9352),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 21, 20, 7, 0, 989, DateTimeKind.Local).AddTicks(1956));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 978, DateTimeKind.Local).AddTicks(2094),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 36, DateTimeKind.Local).AddTicks(3382));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 972, DateTimeKind.Local).AddTicks(8081),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 30, DateTimeKind.Local).AddTicks(1791));
        }
    }
}
