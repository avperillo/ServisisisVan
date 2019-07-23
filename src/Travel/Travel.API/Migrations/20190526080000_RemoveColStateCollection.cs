using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.API.Migrations
{
    public partial class RemoveColStateCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_CollectionState_StateId",
                table: "Collections");

            migrationBuilder.DropTable(
                name: "CollectionState");

            migrationBuilder.DropIndex(
                name: "IX_Collections_StateId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "IdCollection",
                table: "Entry");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Collections");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 809, DateTimeKind.Local).AddTicks(9199),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 21, 20, 7, 0, 989, DateTimeKind.Local).AddTicks(1956));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 827, DateTimeKind.Local).AddTicks(4835),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 36, DateTimeKind.Local).AddTicks(3382));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 821, DateTimeKind.Local).AddTicks(6498),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 30, DateTimeKind.Local).AddTicks(1791));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Trips",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 21, 20, 7, 0, 989, DateTimeKind.Local).AddTicks(1956),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 809, DateTimeKind.Local).AddTicks(9199));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Refuels",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 36, DateTimeKind.Local).AddTicks(3382),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 827, DateTimeKind.Local).AddTicks(4835));

            migrationBuilder.AddColumn<int>(
                name: "IdCollection",
                table: "Entry",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Collections",
                nullable: false,
                defaultValue: new DateTime(2019, 2, 21, 20, 7, 1, 30, DateTimeKind.Local).AddTicks(1791),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 5, 26, 9, 59, 59, 821, DateTimeKind.Local).AddTicks(6498));

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Collections",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Collections_StateId",
                table: "Collections",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_CollectionState_StateId",
                table: "Collections",
                column: "StateId",
                principalTable: "CollectionState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
