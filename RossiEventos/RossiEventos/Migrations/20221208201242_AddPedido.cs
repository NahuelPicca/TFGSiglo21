using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsignacionId = table.Column<int>(type: "int", nullable: false),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NroPedido = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Factura = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_AsignacionVehicTransp_AsignacionId",
                        column: x => x.AsignacionId,
                        principalTable: "AsignacionVehicTransp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_AsignacionId",
                table: "Pedidos",
                column: "AsignacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_Factura",
                table: "Pedidos",
                column: "Factura",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_NroPedido",
                table: "Pedidos",
                column: "NroPedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ReservaId",
                table: "Pedidos",
                column: "ReservaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
