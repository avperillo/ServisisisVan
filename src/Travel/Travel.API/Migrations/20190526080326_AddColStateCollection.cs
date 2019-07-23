using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class AddColStateCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 773, DateTimeKind.Local).AddTicks(1217),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 809, DateTimeKind.Local).AddTicks(9199));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 793, DateTimeKind.Local).AddTicks(9466),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 827, DateTimeKind.Local).AddTicks(4835));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 785, DateTimeKind.Local).AddTicks(4355),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 821, DateTimeKind.Local).AddTicks(6498));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Collections",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CollectionStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Name = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionStates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collections_StateId",
                table: "Collections",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_CollectionStates_StateId",
                table: "Collections",
                column: "StateId",
                principalTable: "CollectionStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_CollectionStates_StateId",
                table: "Collections");

            migrationBuilder.DropTable(
                name: "CollectionStates");

            migrationBuilder.DropIndex(
                name: "IX_Collections_StateId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Collections");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 809, DateTimeKind.Local).AddTicks(9199),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 773, DateTimeKind.Local).AddTicks(1217));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 827, DateTimeKind.Local).AddTicks(4835),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 793, DateTimeKind.Local).AddTicks(9466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 821, DateTimeKind.Local).AddTicks(6498),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 785, DateTimeKind.Local).AddTicks(4355));
        }
    }
}
