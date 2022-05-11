using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addapprovedid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerApprovedId",
                table: "UserVacationDtos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerApprovedId",
                table: "UserShiftBreakDtos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerApprovedId",
                table: "UserDebitDtos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerApprovedId",
                table: "UserCostDtos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerApprovedId",
                table: "UserVacationDtos");

            migrationBuilder.DropColumn(
                name: "ManagerApprovedId",
                table: "UserShiftBreakDtos");

            migrationBuilder.DropColumn(
                name: "ManagerApprovedId",
                table: "UserDebitDtos");

            migrationBuilder.DropColumn(
                name: "ManagerApprovedId",
                table: "UserCostDtos");
        }
    }
}
