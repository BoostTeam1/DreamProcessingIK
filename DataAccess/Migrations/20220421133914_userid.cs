using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVacationDtos_AspNetUsers_UserId1",
                table: "UserVacationDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserVacationDtos_UserId1",
                table: "UserVacationDtos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserVacationDtos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserVacationDtos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserVacationDtos_UserId",
                table: "UserVacationDtos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVacationDtos_AspNetUsers_UserId",
                table: "UserVacationDtos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVacationDtos_AspNetUsers_UserId",
                table: "UserVacationDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserVacationDtos_UserId",
                table: "UserVacationDtos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserVacationDtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserVacationDtos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserVacationDtos_UserId1",
                table: "UserVacationDtos",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVacationDtos_AspNetUsers_UserId1",
                table: "UserVacationDtos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
