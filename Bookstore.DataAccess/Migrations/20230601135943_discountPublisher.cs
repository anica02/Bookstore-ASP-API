using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.DataAccess.Migrations
{
    public partial class discountPublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDiscounts_Books_BookId",
                table: "BookDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Books_BookId",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Prices",
                newName: "BookPublisheId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_BookId",
                table: "Prices",
                newName: "IX_Prices_BookPublisheId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookDiscounts",
                newName: "BookPublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_BookDiscounts_BookId",
                table: "BookDiscounts",
                newName: "IX_BookDiscounts_BookPublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDiscounts_BookPublishers_BookPublisherId",
                table: "BookDiscounts",
                column: "BookPublisherId",
                principalTable: "BookPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_BookPublishers_BookPublisheId",
                table: "Prices",
                column: "BookPublisheId",
                principalTable: "BookPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookDiscounts_BookPublishers_BookPublisherId",
                table: "BookDiscounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_BookPublishers_BookPublisheId",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "BookPublisheId",
                table: "Prices",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_Prices_BookPublisheId",
                table: "Prices",
                newName: "IX_Prices_BookId");

            migrationBuilder.RenameColumn(
                name: "BookPublisherId",
                table: "BookDiscounts",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookDiscounts_BookPublisherId",
                table: "BookDiscounts",
                newName: "IX_BookDiscounts_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookDiscounts_Books_BookId",
                table: "BookDiscounts",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Books_BookId",
                table: "Prices",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
