using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddCantidadSaldoUbi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "SaldoUbicacion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "SaldoUbicacion");
        }
    }
}
