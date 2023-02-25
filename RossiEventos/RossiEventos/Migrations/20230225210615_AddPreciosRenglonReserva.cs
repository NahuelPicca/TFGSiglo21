using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddPreciosRenglonReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CostoTotal",
                table: "RenglonReserva",
                newName: "PrecioTotal");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnit",
                table: "RenglonReserva",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecioUnit",
                table: "RenglonReserva");

            migrationBuilder.RenameColumn(
                name: "PrecioTotal",
                table: "RenglonReserva",
                newName: "CostoTotal");
        }
    }
}
