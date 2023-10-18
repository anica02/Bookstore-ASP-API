using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.DataAccess.Migrations
{
    public partial class publisherEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_BookPublishers_BookPublisheId",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "BookPublisheId",
                table: "Prices",
                newName: "BookPublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_BookPublisheId",
                table: "Prices",
                newName: "IX_Prices_BookPublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_BookPublishers_BookPublisherId",
                table: "Prices",
                column: "BookPublisherId",
                principalTable: "BookPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prices_BookPublishers_BookPublisherId",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "BookPublisherId",
                table: "Prices",
                newName: "BookPublisheId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_BookPublisherId",
                table: "Prices",
                newName: "IX_Prices_BookPublisheId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_BookPublishers_BookPublisheId",
                table: "Prices",
                column: "BookPublisheId",
                principalTable: "BookPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
