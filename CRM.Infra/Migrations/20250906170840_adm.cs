using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class adm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<bool>(
                name: "adm",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

           
            migrationBuilder.AddColumn<bool>(
                name: "adm",
                table: "Lead",
                type: "bit",
                nullable: false,
                defaultValue: false);

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Lead_LeadId",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "adm",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "adm",
                table: "Lead");

            migrationBuilder.AlterColumn<string>(
                name: "NomeProduto",
                table: "PedidoItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Lead_LeadId",
                table: "Pedido",
                column: "LeadId",
                principalTable: "Lead",
                principalColumn: "leadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
