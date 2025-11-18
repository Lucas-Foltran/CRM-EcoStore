using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infra.Migrations
{
    /// <inheritdoc />
    public partial class usuarioTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    sobrenome = table.Column<string>(type: "varchar(200)", nullable: false),
                    email = table.Column<string>(type: "varchar(200)", nullable: false),
                    senha = table.Column<string>(type: "varchar(200)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
