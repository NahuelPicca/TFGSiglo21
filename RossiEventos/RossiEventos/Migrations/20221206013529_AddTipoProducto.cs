using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    FechaInsercion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInserto = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoProducto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producto_TipoId",
                table: "Producto",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_TipoProducto_TipoId",
                table: "Producto",
                column: "TipoId",
                principalTable: "TipoProducto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_TipoProducto_TipoId",
                table: "Producto");

            migrationBuilder.DropTable(
                name: "TipoProducto");

            migrationBuilder.DropIndex(
                name: "IX_Producto_TipoId",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Producto");
        }
    }
}
