using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class ReemplazaTitularViejoPorNuevo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TitularId",
                table: "Vehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_TitularId",
                table: "Vehiculo",
                column: "TitularId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Titular_TitularId",
                table: "Vehiculo",
                column: "TitularId",
                principalTable: "Titular",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Titular_TitularId",
                table: "Vehiculo");

            migrationBuilder.DropIndex(
                name: "IX_Vehiculo_TitularId",
                table: "Vehiculo");

            migrationBuilder.DropColumn(
                name: "TitularId",
                table: "Vehiculo");
        }
    }
}
