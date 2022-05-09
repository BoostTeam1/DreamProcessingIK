using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class useridd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCostDtos_AspNetUsers_UserId1",
                table: "UserCostDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDebitDtos_AspNetUsers_UserId1",
                table: "UserDebitDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserShiftBreakDtos_AspNetUsers_UserId1",
                table: "UserShiftBreakDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserShiftBreakDtos_UserId1",
                table: "UserShiftBreakDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserDebitDtos_UserId1",
                table: "UserDebitDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserCostDtos_UserId1",
                table: "UserCostDtos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserShiftBreakDtos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserDebitDtos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserCostDtos");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserShiftBreakDtos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserDebitDtos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserCostDtos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserShiftBreakDtos_UserId",
                table: "UserShiftBreakDtos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDebitDtos_UserId",
                table: "UserDebitDtos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCostDtos_UserId",
                table: "UserCostDtos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCostDtos_AspNetUsers_UserId",
                table: "UserCostDtos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDebitDtos_AspNetUsers_UserId",
                table: "UserDebitDtos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserShiftBreakDtos_AspNetUsers_UserId",
                table: "UserShiftBreakDtos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCostDtos_AspNetUsers_UserId",
                table: "UserCostDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDebitDtos_AspNetUsers_UserId",
                table: "UserDebitDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserShiftBreakDtos_AspNetUsers_UserId",
                table: "UserShiftBreakDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserShiftBreakDtos_UserId",
                table: "UserShiftBreakDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserDebitDtos_UserId",
                table: "UserDebitDtos");

            migrationBuilder.DropIndex(
                name: "IX_UserCostDtos_UserId",
                table: "UserCostDtos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserShiftBreakDtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserShiftBreakDtos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserDebitDtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserDebitDtos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserCostDtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserCostDtos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserShiftBreakDtos_UserId1",
                table: "UserShiftBreakDtos",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserDebitDtos_UserId1",
                table: "UserDebitDtos",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserCostDtos_UserId1",
                table: "UserCostDtos",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCostDtos_AspNetUsers_UserId1",
                table: "UserCostDtos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDebitDtos_AspNetUsers_UserId1",
                table: "UserDebitDtos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserShiftBreakDtos_AspNetUsers_UserId1",
                table: "UserShiftBreakDtos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
