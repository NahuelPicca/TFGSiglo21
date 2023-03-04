using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequiredAsignacionPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_AsignacionVehicTransp_AsignacionId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "AsignacionId",
                table: "Pedidos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_AsignacionVehicTransp_AsignacionId",
                table: "Pedidos",
                column: "AsignacionId",
                principalTable: "AsignacionVehicTransp",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_AsignacionVehicTransp_AsignacionId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "AsignacionId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_AsignacionVehicTransp_AsignacionId",
                table: "Pedidos",
                column: "AsignacionId",
                principalTable: "AsignacionVehicTransp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
