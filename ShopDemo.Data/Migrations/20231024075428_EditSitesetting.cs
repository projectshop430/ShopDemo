using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopDemo.Data.Migrations
{
    public partial class EditSitesetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutUs",
                table: "SiteSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "SiteSettings");
        }
    }
}
