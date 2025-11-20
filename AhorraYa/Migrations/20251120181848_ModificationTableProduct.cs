using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhorraYa.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ModificationTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.AddColumn<decimal>(
                name: "BarCode",
                table: "Products",
                type: "decimal(18,2)",
                maxLength: 13,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarCode",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");
        }
    }
}
