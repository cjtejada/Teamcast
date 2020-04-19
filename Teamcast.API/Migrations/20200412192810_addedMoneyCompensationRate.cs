using Microsoft.EntityFrameworkCore.Migrations;

namespace Teamcast.Migrations
{
    public partial class addedMoneyCompensationRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinMembers",
                table: "Event");

            migrationBuilder.AddColumn<short>(
                name: "MoneyCompensationRate",
                table: "Event",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyCompensationRate",
                table: "Event");

            migrationBuilder.AddColumn<short>(
                name: "MinMembers",
                table: "Event",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
