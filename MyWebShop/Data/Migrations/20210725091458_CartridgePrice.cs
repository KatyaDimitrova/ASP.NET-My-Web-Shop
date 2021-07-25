using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWebShop.Data.Migrations
{
    public partial class CartridgePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Cartridges",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "AllCartridgesViewModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Printer",
                table: "AllCartridgesViewModel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "AllCartridgesViewModel");

            migrationBuilder.DropColumn(
                name: "Printer",
                table: "AllCartridgesViewModel");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Cartridges",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);
        }
    }
}
