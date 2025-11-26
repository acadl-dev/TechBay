using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBayV01.Data.Migrations
{
    /// <inheritdoc />
    public partial class ajustesNoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Produto",
                newName: "PrecoUnitario");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Produto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataModificação",
                table: "Produto",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Produto",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Produto",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoEstoque",
                table: "Produto",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "DataModificação",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "PrecoEstoque",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produto");

            migrationBuilder.RenameColumn(
                name: "PrecoUnitario",
                table: "Produto",
                newName: "Preco");
        }
    }
}
