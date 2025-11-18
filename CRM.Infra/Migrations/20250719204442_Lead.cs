using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Lead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lead",
                columns: table => new
                {
                    leadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeContato = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    telefone1 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    telefone2 = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    formaPagto = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    pgto = table.Column<bool>(type: "bit", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    link = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    token = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    urlArquivo = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    nomeArquivo = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    cep = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: true),
                    endereco = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    numero = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    complemento = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    bairro = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    estado = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    statusCadastro = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    dataUltimaAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dataCadastro = table.Column<DateTime>(type: "datetime2", nullable: true),
                    linkPdf = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    linkQRCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    linkBarCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    dataVencimentoBoleto = table.Column<DateTime>(type: "datetime2", nullable: true),
                    codigoBarrasBoleto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead", x => x.leadId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lead");
        }
    }
}
