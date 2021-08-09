using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Shoalace.Infra.Migrations
{
    public partial class MembroEvento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Grupo_GrupoId",
                table: "Evento");

            migrationBuilder.DropColumn(
                name: "Comparecer",
                table: "Membro");

            migrationBuilder.AlterColumn<long>(
                name: "GrupoId",
                table: "Evento",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "MembroEvento",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<long>(type: "bigint", nullable: false),
                    EventoId = table.Column<long>(type: "bigint", nullable: false),
                    Comparecer = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false),
                    Cadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Alterado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembroEvento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembroEvento_Evento_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Evento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembroEvento_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembroEvento_EventoId",
                table: "MembroEvento",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_MembroEvento_UsuarioId",
                table: "MembroEvento",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Grupo_GrupoId",
                table: "Evento",
                column: "GrupoId",
                principalTable: "Grupo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Grupo_GrupoId",
                table: "Evento");

            migrationBuilder.DropTable(
                name: "MembroEvento");

            migrationBuilder.AddColumn<int>(
                name: "Comparecer",
                table: "Membro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "GrupoId",
                table: "Evento",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Grupo_GrupoId",
                table: "Evento",
                column: "GrupoId",
                principalTable: "Grupo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
