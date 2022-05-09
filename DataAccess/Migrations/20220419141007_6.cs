using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanySectorDto_Companies_CompanyId",
                table: "CompanySectorDto");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySectorDto_Sector_SectorId",
                table: "CompanySectorDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sector",
                table: "Sector");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySectorDto",
                table: "CompanySectorDto");

            migrationBuilder.RenameTable(
                name: "Sector",
                newName: "Sectors");

            migrationBuilder.RenameTable(
                name: "CompanySectorDto",
                newName: "CompanySectorDtos");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySectorDto_SectorId",
                table: "CompanySectorDtos",
                newName: "IX_CompanySectorDtos_SectorId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySectorDto_CompanyId",
                table: "CompanySectorDtos",
                newName: "IX_CompanySectorDtos_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySectorDtos",
                table: "CompanySectorDtos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySectorDtos_Companies_CompanyId",
                table: "CompanySectorDtos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySectorDtos_Sectors_SectorId",
                table: "CompanySectorDtos",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanySectorDtos_Companies_CompanyId",
                table: "CompanySectorDtos");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanySectorDtos_Sectors_SectorId",
                table: "CompanySectorDtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sectors",
                table: "Sectors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanySectorDtos",
                table: "CompanySectorDtos");

            migrationBuilder.RenameTable(
                name: "Sectors",
                newName: "Sector");

            migrationBuilder.RenameTable(
                name: "CompanySectorDtos",
                newName: "CompanySectorDto");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySectorDtos_SectorId",
                table: "CompanySectorDto",
                newName: "IX_CompanySectorDto_SectorId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanySectorDtos_CompanyId",
                table: "CompanySectorDto",
                newName: "IX_CompanySectorDto_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sector",
                table: "Sector",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanySectorDto",
                table: "CompanySectorDto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySectorDto_Companies_CompanyId",
                table: "CompanySectorDto",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanySectorDto_Sector_SectorId",
                table: "CompanySectorDto",
                column: "SectorId",
                principalTable: "Sector",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
