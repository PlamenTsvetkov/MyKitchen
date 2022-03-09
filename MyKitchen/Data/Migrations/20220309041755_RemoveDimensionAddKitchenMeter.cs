using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKitchen.Data.Migrations
{
    public partial class RemoveDimensionAddKitchenMeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitchens_Dimensions_DimensionId",
                table: "Kitchens");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropIndex(
                name: "IX_Kitchens_DimensionId",
                table: "Kitchens");

            migrationBuilder.DropColumn(
                name: "DimensionId",
                table: "Kitchens");

            migrationBuilder.AddColumn<double>(
                name: "KitchenMeter",
                table: "Kitchens",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KitchenMeter",
                table: "Kitchens");

            migrationBuilder.AddColumn<int>(
                name: "DimensionId",
                table: "Kitchens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KitchenId = table.Column<int>(type: "int", nullable: false),
                    LenghtCenter = table.Column<double>(type: "float", nullable: false),
                    LenghtG = table.Column<double>(type: "float", nullable: false),
                    LenghtIsland = table.Column<double>(type: "float", nullable: false),
                    LenghtLeft = table.Column<double>(type: "float", nullable: false),
                    LenghtRight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kitchens_DimensionId",
                table: "Kitchens",
                column: "DimensionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Kitchens_Dimensions_DimensionId",
                table: "Kitchens",
                column: "DimensionId",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
