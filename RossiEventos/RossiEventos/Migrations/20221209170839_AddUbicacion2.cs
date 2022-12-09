using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddUbicacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ubicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Estante = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Columna = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Fila = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Habilitado = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicacion", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ubicacion_Codigo",
                table: "Ubicacion",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ubicacion");
        }
    }
}
