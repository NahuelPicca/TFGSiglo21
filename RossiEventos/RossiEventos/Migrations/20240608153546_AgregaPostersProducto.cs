using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AgregaPostersProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Poster1",
                table: "Producto",
                type: "varchar",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Poster2",
                table: "Producto",
                type: "varchar",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Poster3",
                table: "Producto",
                type: "varchar",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster1",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Poster2",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "Poster3",
                table: "Producto");
        }
    }
}
