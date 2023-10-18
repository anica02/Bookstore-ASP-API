using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.DataAccess.Migrations
{
    public partial class cartItemsBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Books_BookId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Books_BookId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "OrderItems",
                newName: "BookPublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems",
                newName: "IX_OrderItems_BookPublisherId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "CartItems",
                newName: "BookPublisherId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_BookId",
                table: "CartItems",
                newName: "IX_CartItems_BookPublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_BookPublishers_BookPublisherId",
                table: "CartItems",
                column: "BookPublisherId",
                principalTable: "BookPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_BookPublishers_BookPublisherId",
                table: "OrderItems",
                column: "BookPublisherId",
                principalTable: "BookPublishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_BookPublishers_BookPublisherId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_BookPublishers_BookPublisherId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "BookPublisherId",
                table: "OrderItems",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_BookPublisherId",
                table: "OrderItems",
                newName: "IX_OrderItems_BookId");

            migrationBuilder.RenameColumn(
                name: "BookPublisherId",
                table: "CartItems",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_BookPublisherId",
                table: "CartItems",
                newName: "IX_CartItems_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Books_BookId",
                table: "CartItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Books_BookId",
                table: "OrderItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
