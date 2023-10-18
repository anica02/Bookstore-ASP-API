using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.DataAccess.Migrations
{
    public partial class fileChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_BookPublisherId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BookPublisherId",
                table: "Files",
                column: "BookPublisherId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_BookPublisherId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BookPublisherId",
                table: "Files",
                column: "BookPublisherId");
        }
    }
}
