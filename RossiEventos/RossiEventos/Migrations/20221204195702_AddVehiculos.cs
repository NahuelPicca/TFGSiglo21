using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddVehiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patente = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false),
                    Marca = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    FechaVencPoliza = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NroPoliza = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    TitularId = table.Column<int>(type: "int", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculo_Titulares_TitularId",
                        column: x => x.TitularId,
                        principalTable: "Titulares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_NroPoliza",
                table: "Vehiculo",
                column: "NroPoliza",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_Patente",
                table: "Vehiculo",
                column: "Patente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_TitularId",
                table: "Vehiculo",
                column: "TitularId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculo");
        }
    }
}
