using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class ChangeChargeEnityToCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropSequence(
                name: "charge_sequence");

            migrationBuilder.CreateSequence(
                name: "collection_sequence",
                incrementBy: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 839, DateTimeKind.Local).AddTicks(9352),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 578, DateTimeKind.Local).AddTicks(6095));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 978, DateTimeKind.Local).AddTicks(2094),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 602, DateTimeKind.Local).AddTicks(1829));

            migrationBuilder.CreateTable(
                name: "CollectionState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 972, DateTimeKind.Local).AddTicks(8081)),
                    StateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_CollectionState_StateId",
                        column: x => x.StateId,
                        principalTable: "CollectionState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCollection = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IdTraveler = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    CollectionId = table.Column<int>(nullable: true)
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
                name: "IX_Collections_StateId",
                table: "Collections",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_CollectionId",
                table: "Entry",
                column: "CollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entry");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "CollectionState");

            migrationBuilder.DropSequence(
                name: "collection_sequence");

            migrationBuilder.CreateSequence(
                name: "charge_sequence",
                incrementBy: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 578, DateTimeKind.Local).AddTicks(6095),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 839, DateTimeKind.Local).AddTicks(9352));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 602, DateTimeKind.Local).AddTicks(1829),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 14, 20, 33, 49, 978, DateTimeKind.Local).AddTicks(2094));

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 2, 6, 20, 10, 6, 596, DateTimeKind.Local).AddTicks(1243)),
                    IdTraveler = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                });
        }
    }
}
