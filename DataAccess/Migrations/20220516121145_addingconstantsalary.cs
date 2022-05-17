using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addingconstantsalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonelDocuments_AspNetUsers_AppUserId",
                table: "PersonelDocuments");

            migrationBuilder.DropColumn(
                name: "WagePerHour",
                table: "Costs");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "PersonelDocuments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<short>(
                name: "ConstantSalary",
                table: "AspNetUsers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonelDocuments_AspNetUsers_AppUserId",
                table: "PersonelDocuments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonelDocuments_AspNetUsers_AppUserId",
                table: "PersonelDocuments");

            migrationBuilder.DropColumn(
                name: "ConstantSalary",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "PersonelDocuments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<short>(
                name: "WagePerHour",
                table: "Costs",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonelDocuments_AspNetUsers_AppUserId",
                table: "PersonelDocuments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
