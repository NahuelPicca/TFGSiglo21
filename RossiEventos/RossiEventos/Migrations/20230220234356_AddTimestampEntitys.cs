using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampEntitys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Vehiculo",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Ubicacion",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "TipoProducto",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SeguimientoPedido",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "SaldoUbicacion",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Reservas",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "RenglonMov",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "RenglonDeReserva",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Producto",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Persona",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Pedidos",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "EncabezadoMovStk",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Deposito",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ComprobanteEntrega",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Categoria",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Calidad",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AsignacionVehicTransp",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "TipoProducto");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SeguimientoPedido");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "SaldoUbicacion");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "RenglonMov");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "RenglonDeReserva");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "EncabezadoMovStk");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Deposito");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ComprobanteEntrega");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Calidad");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AsignacionVehicTransp");
        }
    }
}
