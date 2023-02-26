using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoCalculadoRenglonReserva : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioTotal",
                table: "RenglonReserva",
                type: "decimal(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                computedColumnSql: "Cantidad*PrecioUnit",
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldPrecision: 15,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioTotal",
                table: "RenglonReserva",
                type: "decimal(15,2)",
                precision: 15,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(15,2)",
                oldPrecision: 15,
                oldScale: 2,
                oldComputedColumnSql: "Cantidad*PrecioUnit");
        }
    }
}
