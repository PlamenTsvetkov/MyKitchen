using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKitchen.Data.Migrations
{
    public partial class KitchenTableIsPublicColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Kitchens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Kitchens");
        }
    }
}
