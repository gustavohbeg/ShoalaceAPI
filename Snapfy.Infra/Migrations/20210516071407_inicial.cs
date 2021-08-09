using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Shoalace.Infra.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acesso",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acesso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Erro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Parametros = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aniversario = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<byte>(type: "tinyint", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<byte>(type: "tinyint", nullable: false),
                    Publico = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grupo_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    DiaInteiro = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Repetir = table.Column<int>(type: "int", nullable: false),
                    Notificar = table.Column<int>(type: "int", nullable: false),
                    GrupoId = table.Column<long>(type: "bigint", nullable: false),
                    Foto = table.Column<byte>(type: "tinyint", nullable: false),
                    Publico = table.Column<bool>(type: "bit", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evento_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Membro",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    GrupoId = table.Column<long>(type: "bigint", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    Comparecer = table.Column<int>(type: "int", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Membro_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Membro_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensagem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    UsuarioDestinoId = table.Column<long>(type: "bigint", nullable: false),
                    GrupoId = table.Column<long>(type: "bigint", nullable: false),
                    Audio = table.Column<byte>(type: "tinyint", nullable: false),
                    Foto = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagem_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensagem_Usuario_UsuarioDestinoId",
                        column: x => x.UsuarioDestinoId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensagem_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusMensagem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembroId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MensagemId = table.Column<long>(type: "bigint", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusMensagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusMensagem_Membro_MembroId",
                        column: x => x.MembroId,
                        principalTable: "Membro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatusMensagem_Mensagem_MensagemId",
                        column: x => x.MensagemId,
                        principalTable: "Mensagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_GrupoId",
                table: "Evento",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_UsuarioId",
                table: "Grupo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Membro_GrupoId",
                table: "Membro",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Membro_UsuarioId",
                table: "Membro",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_GrupoId",
                table: "Mensagem",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_UsuarioDestinoId",
                table: "Mensagem",
                column: "UsuarioDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_UsuarioId",
                table: "Mensagem",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusMensagem_MembroId",
                table: "StatusMensagem",
                column: "MembroId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusMensagem_MensagemId",
                table: "StatusMensagem",
                column: "MensagemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acesso");

            migrationBuilder.DropTable(
                name: "Erro");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "StatusMensagem");

            migrationBuilder.DropTable(
                name: "Membro");

            migrationBuilder.DropTable(
                name: "Mensagem");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
