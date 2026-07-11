using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenseGesture.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaVendaCancelamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FormaPagamento",
                table: "Vendas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Anulada",
                table: "Vendas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AnuladaPor",
                table: "Vendas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CanceladaEm",
                table: "Vendas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MotivoCancelamento",
                table: "Vendas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoProduto",
                table: "Produtos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anulada",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "AnuladaPor",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "CanceladaEm",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "MotivoCancelamento",
                table: "Vendas");

            migrationBuilder.AlterColumn<string>(
                name: "FormaPagamento",
                table: "Vendas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoProduto",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
