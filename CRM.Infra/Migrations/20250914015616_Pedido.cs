using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Pedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Pedido",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Aguardando Pagamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pedido");
        }
    }
}
