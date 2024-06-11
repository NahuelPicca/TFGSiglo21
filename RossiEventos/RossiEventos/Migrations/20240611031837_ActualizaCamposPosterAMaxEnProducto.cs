using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class ActualizaCamposPosterAMaxEnProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Poster3",
                table: "Producto",
                type: "varchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "Poster2",
                table: "Producto",
                type: "varchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");

            migrationBuilder.AlterColumn<string>(
                name: "Poster1",
                table: "Producto",
                type: "varchar(MAX)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Poster3",
                table: "Producto",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Poster2",
                table: "Producto",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)");

            migrationBuilder.AlterColumn<string>(
                name: "Poster1",
                table: "Producto",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(MAX)");
        }
    }
}
