using Microsoft.EntityFrameworkCore.Migrations;

namespace LabProject.Migrations.Cart
{
    public partial class CartMigr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdInBase",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdInBase",
                table: "CartItems");
        }
    }
}
