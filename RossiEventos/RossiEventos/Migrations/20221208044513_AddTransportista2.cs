using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportista2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transportista",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FechaVencLicencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Licencia = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportista_Persona_Id",
                        column: x => x.Id,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transportista");
        }
    }
}
