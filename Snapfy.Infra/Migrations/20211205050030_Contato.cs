using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shoalace.Infra.Migrations
{
    public partial class Contato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioContatoId = table.Column<long>(type: "bigint", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_Usuario_UsuarioContatoId",
                        column: x => x.UsuarioContatoId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contato_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_UsuarioContatoId",
                table: "Contato",
                column: "UsuarioContatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_UsuarioId",
                table: "Contato",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contato");
        }
    }
}
