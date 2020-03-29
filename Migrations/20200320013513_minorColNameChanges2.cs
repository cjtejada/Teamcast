using Microsoft.EntityFrameworkCore.Migrations;

namespace Teamcast.Migrations
{
    public partial class minorColNameChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Users_CreatorUserId",
                table: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "CreatorUserId",
                table: "TeamMembers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_CreatorUserId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Users_UserId",
                table: "TeamMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Users_UserId",
                table: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TeamMembers",
                newName: "CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_UserId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Users_CreatorUserId",
                table: "TeamMembers",
                column: "CreatorUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
