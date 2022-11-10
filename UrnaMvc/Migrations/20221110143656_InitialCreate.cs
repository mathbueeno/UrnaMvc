using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrnaMvc.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidate",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_completo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nome_vice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    data_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    legenda = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voting",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    candidate_id = table.Column<int>(type: "int", nullable: true),
                    data_voto = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voting", x => x.id);
                    table.ForeignKey(
                        name: "FK_voting_candidate_candidate_id",
                        column: x => x.candidate_id,
                        principalTable: "candidate",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_voting_candidate_id",
                table: "voting",
                column: "candidate_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voting");

            migrationBuilder.DropTable(
                name: "candidate");
        }
    }
}
