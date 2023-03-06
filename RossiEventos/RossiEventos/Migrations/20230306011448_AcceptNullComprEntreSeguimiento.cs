using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AcceptNullComprEntreSeguimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeguimientoPedido_ComprobanteEntrega_ComproEntreId",
                table: "SeguimientoPedido");

            migrationBuilder.AlterColumn<int>(
                name: "ComproEntreId",
                table: "SeguimientoPedido",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SeguimientoPedido_ComprobanteEntrega_ComproEntreId",
                table: "SeguimientoPedido",
                column: "ComproEntreId",
                principalTable: "ComprobanteEntrega",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeguimientoPedido_ComprobanteEntrega_ComproEntreId",
                table: "SeguimientoPedido");

            migrationBuilder.AlterColumn<int>(
                name: "ComproEntreId",
                table: "SeguimientoPedido",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SeguimientoPedido_ComprobanteEntrega_ComproEntreId",
                table: "SeguimientoPedido",
                column: "ComproEntreId",
                principalTable: "ComprobanteEntrega",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
