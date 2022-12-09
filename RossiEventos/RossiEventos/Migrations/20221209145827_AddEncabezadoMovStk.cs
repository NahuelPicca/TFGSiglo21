using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddEncabezadoMovStk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncabezadoMovStk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    NroComprobante = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false),
                    ComprobanteRelacionado = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncabezadoMovStk", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncabezadoMovStk_TipoMovimiento_NroComprobante",
                table: "EncabezadoMovStk",
                columns: new[] { "TipoMovimiento", "NroComprobante" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncabezadoMovStk");
        }
    }
}
