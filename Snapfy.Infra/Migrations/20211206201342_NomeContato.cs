using Microsoft.EntityFrameworkCore.Migrations;

namespace Shoalace.Infra.Migrations
{
    public partial class NomeContato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "UsuarioContatoId",
                table: "Contato",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Contato",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Contato",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Contato");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioContatoId",
                table: "Contato",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
