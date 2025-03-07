using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoodsManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddGhiChuToGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ghi_chu",
                table: "Goods",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ghi_chu",
                table: "Goods");
        }
    }
}
