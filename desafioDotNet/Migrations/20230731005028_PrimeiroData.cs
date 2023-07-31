using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace desafioDotNet.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiroData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    Cartao = table.Column<string>(type: "text", nullable: false),
                    DonoLoja = table.Column<string>(type: "text", nullable: false),
                    NomeLoja = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Register");
        }
    }
}
