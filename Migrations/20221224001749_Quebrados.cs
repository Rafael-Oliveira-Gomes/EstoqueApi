using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstoqueApi.Migrations
{
    public partial class Quebrados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserUltimoUpdate",
                table: "Produto",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "QtdFuncional",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QtdQuebrado",
                table: "Produto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtdFuncional",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "QtdQuebrado",
                table: "Produto");

            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "UserUltimoUpdate",
                keyValue: null,
                column: "UserUltimoUpdate",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserUltimoUpdate",
                table: "Produto",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
