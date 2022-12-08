using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class EliminaTitularViejo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Titulares_TitularId",
                table: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Titulares");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_TitularId",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "TitularId",
                table: "Vehiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TitularId",
                table: "Vehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Titulares",
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
                    Localidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    NroDni = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    TipoDni = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titulares", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_TitularId",
                table: "Vehiculo",
                column: "TitularId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Titulares_TitularId",
                table: "Vehiculo",
                column: "TitularId",
                principalTable: "Titulares",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
