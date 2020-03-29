using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Teamcast.Migrations
{
    public partial class addedLocationPropInEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Events");
        }
    }
}
