using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameGhiChuColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ghi_chu",
                table: "Goods",
                newName: "GhiChu");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GhiChu",
                table: "Goods",
                newName: "ghi_chu");
        }

    }
}
