using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCompra.API.Migrations
{
    public partial class Migracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Categoria = table.Column<int>(nullable: false),
                    Preco = table.Column<decimal>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Situacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacaoCompra",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsuarioSolicitante = table.Column<string>(nullable: true),
                    NomeFornecedor = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    TotalGeral = table.Column<decimal>(nullable: true),
                    Situacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoCompra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensSolicitacaoCompra",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(nullable: false),
                    SolicitacaoCompraId = table.Column<Guid>(nullable: false),
                    Qtde = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensSolicitacaoCompra", x => new { x.ProdutoId, x.SolicitacaoCompraId });
                    table.ForeignKey(
                        name: "FK_ItensSolicitacaoCompra_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensSolicitacaoCompra_SolicitacaoCompra_SolicitacaoCompraId",
                        column: x => x.SolicitacaoCompraId,
                        principalTable: "SolicitacaoCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensSolicitacaoCompra_SolicitacaoCompraId",
                table: "ItensSolicitacaoCompra",
                column: "SolicitacaoCompraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensSolicitacaoCompra");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "SolicitacaoCompra");
        }
    }
}
