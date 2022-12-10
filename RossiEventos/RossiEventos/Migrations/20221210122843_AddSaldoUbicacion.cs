using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddSaldoUbicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaldoUbicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositoId = table.Column<int>(type: "int", nullable: false),
                    UbicacionId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaldoUbicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaldoUbicacion_Deposito_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Deposito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaldoUbicacion_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaldoUbicacion_Ubicacion_UbicacionId",
                        column: x => x.UbicacionId,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaldoUbicacion_DepositoId",
                table: "SaldoUbicacion",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_SaldoUbicacion_ProductoId",
                table: "SaldoUbicacion",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_SaldoUbicacion_UbicacionId",
                table: "SaldoUbicacion",
                column: "UbicacionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaldoUbicacion");
        }
    }
}
