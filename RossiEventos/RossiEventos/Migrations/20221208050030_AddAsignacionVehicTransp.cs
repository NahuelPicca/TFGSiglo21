using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddAsignacionVehicTransp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionVehicTransp_Transportista_TransportitaId",
                table: "AsignacionVehicTransp");

            migrationBuilder.DropIndex(
                name: "IX_Titulares_Cuit",
                table: "Titulares");

            migrationBuilder.DropIndex(
                name: "IX_Titulares_NroDni",
                table: "Titulares");

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

            migrationBuilder.CreateIndex(
                name: "IX_Titulares_Cuit",
                table: "Titulares",
                column: "Cuit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Titulares_NroDni",
                table: "Titulares",
                column: "NroDni",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionVehicTransp_Transportista_TransportitaId",
                table: "AsignacionVehicTransp",
                column: "TransportitaId",
                principalTable: "Transportista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
