using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class ActualizaRelacionTransporteYAsignacionVehiculoTransporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AsignacionVehicTransp_TransportitaId",
                table: "AsignacionVehicTransp",
                column: "TransportitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionVehicTransp_Transportista_TransportitaId",
                table: "AsignacionVehicTransp",
                column: "TransportitaId",
                principalTable: "Transportista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionVehicTransp_Transportista_TransportitaId",
                table: "AsignacionVehicTransp");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionVehicTransp_TransportitaId",
                table: "AsignacionVehicTransp");
        }
    }
}
