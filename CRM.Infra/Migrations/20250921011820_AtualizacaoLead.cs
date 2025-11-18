using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigoBarrasBoleto",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "dataVencimentoBoleto",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "linkBarCode",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "linkPdf",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "linkQRCode",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "nomeArquivo",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "telefone2",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "urlArquivo",
                table: "Lead");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigoBarrasBoleto",
                table: "Lead",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataVencimentoBoleto",
                table: "Lead",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkBarCode",
                table: "Lead",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkPdf",
                table: "Lead",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkQRCode",
                table: "Lead",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nomeArquivo",
                table: "Lead",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "telefone2",
                table: "Lead",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "urlArquivo",
                table: "Lead",
                type: "varchar(300)",
                maxLength: 300,
                nullable: true);
        }
    }
}
