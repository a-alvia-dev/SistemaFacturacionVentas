using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaFacturacionVentas.Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRowVersionProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Productos",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Productos");
        }
    }
}
