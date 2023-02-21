using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RossiEventos.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeHabilitadoUbicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Habilitado",
                table: "Ubicacion",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Habilitado",
                table: "Ubicacion",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
