using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class DeletePropertyEntryForRegenerate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entry");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 103, DateTimeKind.Local).AddTicks(5248),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 773, DateTimeKind.Local).AddTicks(1217));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 131, DateTimeKind.Local).AddTicks(1993),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 793, DateTimeKind.Local).AddTicks(9466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 120, DateTimeKind.Local).AddTicks(5979),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 785, DateTimeKind.Local).AddTicks(4355));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 773, DateTimeKind.Local).AddTicks(1217),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 103, DateTimeKind.Local).AddTicks(5248));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 793, DateTimeKind.Local).AddTicks(9466),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 131, DateTimeKind.Local).AddTicks(1993));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 10, 3, 25, 785, DateTimeKind.Local).AddTicks(4355),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 12, 42, 59, 120, DateTimeKind.Local).AddTicks(5979));

            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<decimal>(nullable: false),
                    CollectionId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IdTraveler = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entry_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entry_CollectionId",
                table: "Entry",
                column: "CollectionId");
        }
    }
}
