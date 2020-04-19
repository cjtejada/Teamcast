using Microsoft.EntityFrameworkCore.Migrations;

namespace Teamcast.Migrations
{
    public partial class changeDateTypesForTypeColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "MinMembers",
                table: "Event",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "MaxMembers",
                table: "Event",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "CompensationType",
                table: "Event",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "CategoryType",
                table: "Event",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MinMembers",
                table: "Event",
                type: "int",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "MaxMembers",
                table: "Event",
                type: "int",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "CompensationType",
                table: "Event",
                type: "int",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<int>(
                name: "CategoryType",
                table: "Event",
                type: "int",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
