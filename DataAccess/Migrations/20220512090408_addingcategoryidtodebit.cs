using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addingcategoryidtodebit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "UserDebitDtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDebitDtos_CategoryId",
                table: "UserDebitDtos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDebitDtos_Categories_CategoryId",
                table: "UserDebitDtos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDebitDtos_Categories_CategoryId",
                table: "UserDebitDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserDebitDtos_CategoryId",
                table: "UserDebitDtos");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "UserDebitDtos");
        }
    }
}
