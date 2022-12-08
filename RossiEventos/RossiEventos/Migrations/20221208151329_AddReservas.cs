using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddReservas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaReserva = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NroReserva = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadPersonas = table.Column<int>(type: "int", nullable: false),
                    DireccionEvento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LocalidadEvento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ProvinciaEvento = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_NroReserva_UsuarioId",
                table: "Reservas",
                columns: new[] { "NroReserva", "UsuarioId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");
        }
    }
}
