using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopDemo.Data.Migrations
{
    public partial class Editsitebaners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannerPlacement",
                table: "siteBanners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerPlacement",
                table: "siteBanners");
        }
    }
}
