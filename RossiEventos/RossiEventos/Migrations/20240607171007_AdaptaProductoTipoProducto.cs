using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class AdaptaProductoTipoProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_TipoProducto_TipoId",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "TipoId",
                table: "Producto",
                newName: "TipoProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_TipoId",
                table: "Producto",
                newName: "IX_Producto_TipoProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_TipoProducto_TipoProductoId",
                table: "Producto",
                column: "TipoProductoId",
                principalTable: "TipoProducto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producto_TipoProducto_TipoProductoId",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "TipoProductoId",
                table: "Producto",
                newName: "TipoId");

            migrationBuilder.RenameIndex(
                name: "IX_Producto_TipoProductoId",
                table: "Producto",
                newName: "IX_Producto_TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producto_TipoProducto_TipoId",
                table: "Producto",
                column: "TipoId",
                principalTable: "TipoProducto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
