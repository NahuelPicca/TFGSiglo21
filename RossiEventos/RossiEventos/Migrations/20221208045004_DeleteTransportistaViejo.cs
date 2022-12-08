using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class DeleteTransportistaViejo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsignacionVehicTransp_Transportistas_TransportitaId",
                table: "AsignacionVehicTransp");

            migrationBuilder.DropTable(
                name: "Transportistas");

            migrationBuilder.DropIndex(
                name: "IX_AsignacionVehicTransp_TransportitaId",
                table: "AsignacionVehicTransp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transportistas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Cuit = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencLicencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    Licencia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Localidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NroDni = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    TipoDni = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportistas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionVehicTransp_TransportitaId",
                table: "AsignacionVehicTransp",
                column: "TransportitaId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportistas_Cuit",
                table: "Transportistas",
                column: "Cuit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportistas_NroDni",
                table: "Transportistas",
                column: "NroDni",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AsignacionVehicTransp_Transportistas_TransportitaId",
                table: "AsignacionVehicTransp",
                column: "TransportitaId",
                principalTable: "Transportistas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
