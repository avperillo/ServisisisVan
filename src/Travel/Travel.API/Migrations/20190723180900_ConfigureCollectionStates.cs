using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class ConfigureCollectionStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_CollectionStates_StateId",
                table: "Collections");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 560, DateTimeKind.Local).AddTicks(8611),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 313, DateTimeKind.Local).AddTicks(1438));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 613, DateTimeKind.Local).AddTicks(9049),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 346, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Entries",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 608, DateTimeKind.Local).AddTicks(2186),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 342, DateTimeKind.Local).AddTicks(8737));

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Collections",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 581, DateTimeKind.Local).AddTicks(9499),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 330, DateTimeKind.Local).AddTicks(6177));

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_CollectionStates_StateId",
                table: "Collections",
                column: "StateId",
                principalTable: "CollectionStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_CollectionStates_StateId",
                table: "Collections");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 313, DateTimeKind.Local).AddTicks(1438),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 560, DateTimeKind.Local).AddTicks(8611));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 346, DateTimeKind.Local).AddTicks(852),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 613, DateTimeKind.Local).AddTicks(9049));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Entries",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 342, DateTimeKind.Local).AddTicks(8737),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 608, DateTimeKind.Local).AddTicks(2186));

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Collections",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 330, DateTimeKind.Local).AddTicks(6177),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 7, 23, 20, 8, 59, 581, DateTimeKind.Local).AddTicks(9499));

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_CollectionStates_StateId",
                table: "Collections",
                column: "StateId",
                principalTable: "CollectionStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
