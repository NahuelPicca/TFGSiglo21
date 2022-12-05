using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class NotDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionVehicTransp_Transportistas_TransportitaId",
                table: "AsignacionVehicTransp");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionVehicTransp_Vehiculo_VehiculoId",
                table: "AsignacionVehicTransp");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionVehicTransp_Transportistas_TransportitaId",
                table: "AsignacionVehicTransp",
                column: "TransportitaId",
                principalTable: "Transportistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionVehicTransp_Vehiculo_VehiculoId",
                table: "AsignacionVehicTransp",
                column: "VehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionVehicTransp_Transportistas_TransportitaId",
                table: "AsignacionVehicTransp");

            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionVehicTransp_Vehiculo_VehiculoId",
                table: "AsignacionVehicTransp");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionVehicTransp_Transportistas_TransportitaId",
                table: "AsignacionVehicTransp",
                column: "TransportitaId",
                principalTable: "Transportistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionVehicTransp_Vehiculo_VehiculoId",
                table: "AsignacionVehicTransp",
                column: "VehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
