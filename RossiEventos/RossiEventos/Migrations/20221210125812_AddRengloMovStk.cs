using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddRengloMovStk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RenglonMov",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    SaldoUbiId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenglonMov", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RenglonMov_EncabezadoMovStk_MovimientoId",
                        column: x => x.MovimientoId,
                        principalTable: "EncabezadoMovStk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RenglonMov_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RenglonMov_SaldoUbicacion_SaldoUbiId",
                        column: x => x.SaldoUbiId,
                        principalTable: "SaldoUbicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenglonMov_MovimientoId",
                table: "RenglonMov",
                column: "MovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_RenglonMov_ProductoId",
                table: "RenglonMov",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_RenglonMov_SaldoUbiId",
                table: "RenglonMov",
                column: "SaldoUbiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenglonMov");
        }
    }
}
