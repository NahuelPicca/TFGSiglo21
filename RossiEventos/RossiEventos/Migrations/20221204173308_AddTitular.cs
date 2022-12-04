using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddTitular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Titulares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    TipoDni = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    NroDni = table.Column<int>(type: "int", nullable: false),
                    Cuit = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Localidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulares", x => x.Id);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Titulares");
        }
    }
}
