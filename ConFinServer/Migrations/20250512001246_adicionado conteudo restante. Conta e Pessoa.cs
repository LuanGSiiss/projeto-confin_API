using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ConFinServer.Migrations
{
    /// <inheritdoc />
    public partial class adicionadoconteudorestanteContaePessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CidadeCodigo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Pessoa_Cidade_CidadeCodigo",
                        column: x => x.CidadeCodigo,
                        principalTable: "Cidade",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conta",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Situacao = table.Column<int>(type: "integer", nullable: false),
                    PessoaCodigo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conta", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_Conta_Pessoa_PessoaCodigo",
                        column: x => x.PessoaCodigo,
                        principalTable: "Pessoa",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conta_PessoaCodigo",
                table: "Conta",
                column: "PessoaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CidadeCodigo",
                table: "Pessoa",
                column: "CidadeCodigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conta");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
