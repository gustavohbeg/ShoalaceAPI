using Microsoft.EntityFrameworkCore.Migrations;

namespace Shoalace.Infra.Migrations
{
    public partial class RemoveUsuarioIdGrupo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupo_Usuario_UsuarioId",
                table: "Grupo");

            migrationBuilder.DropIndex(
                name: "IX_Grupo_UsuarioId",
                table: "Grupo");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Grupo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UsuarioId",
                table: "Grupo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_UsuarioId",
                table: "Grupo",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupo_Usuario_UsuarioId",
                table: "Grupo",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
