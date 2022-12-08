using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUsuarioYCreaIndicesPersona : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_Cuit",
                table: "Persona",
                column: "Cuit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_NroDni",
                table: "Persona",
                column: "NroDni",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Persona_Cuit",
                table: "Persona");

            migrationBuilder.DropIndex(
                name: "IX_Persona_NroDni",
                table: "Persona");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Contraseña = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Cuit = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Localidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    NroDni = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    TipoDni = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Cuit",
                table: "Usuarios",
                column: "Cuit",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NroDni",
                table: "Usuarios",
                column: "NroDni",
                unique: true);
        }
    }
}
