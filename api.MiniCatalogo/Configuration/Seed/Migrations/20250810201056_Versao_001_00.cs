using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.MiniCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class Versao_001_00 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    preco = table.Column<decimal>(type: "NUMERIC(100, 4)", nullable: false),
                    categoria_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_categoria_id",
                        column: x => x.categoria_id,
                        principalTable: "Categoria",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_nome",
                table: "Categoria",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_categoria_id_nome",
                table: "Produto",
                columns: new[] { "categoria_id", "nome" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
