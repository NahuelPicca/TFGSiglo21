using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddUbicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deposito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Direccion = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Localidad = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Provincia = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposito", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deposito_Codigo",
                table: "Deposito",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Deposito");
        }
    }
}
