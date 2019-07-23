using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class AddPropertyEntryForRegenerate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "entry_sequence",
                incrementBy: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 313, DateTimeKind.Local).AddTicks(1438),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 103, DateTimeKind.Local).AddTicks(5248));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 346, DateTimeKind.Local).AddTicks(852),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 131, DateTimeKind.Local).AddTicks(1993));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 330, DateTimeKind.Local).AddTicks(6177),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 120, DateTimeKind.Local).AddTicks(5979));

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 342, DateTimeKind.Local).AddTicks(8737)),
                    IdTraveler = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CollectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CollectionId",
                table: "Entries",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropSequence(
                name: "entry_sequence");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 103, DateTimeKind.Local).AddTicks(5248),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 313, DateTimeKind.Local).AddTicks(1438));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 131, DateTimeKind.Local).AddTicks(1993),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 346, DateTimeKind.Local).AddTicks(852));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 120, DateTimeKind.Local).AddTicks(5979),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 45, 12, 330, DateTimeKind.Local).AddTicks(6177));
        }
    }
}
