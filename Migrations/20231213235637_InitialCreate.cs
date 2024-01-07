using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCOREgrp05.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    matchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    equipe1 = table.Column<string>(type: "TEXT", nullable: true),
                    equipe2 = table.Column<string>(type: "TEXT", nullable: true),
                    score = table.Column<string>(type: "TEXT", nullable: false),
                    temps = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.matchId);
                });

            migrationBuilder.CreateTable(
                name: "But",
                columns: table => new
                {
                    butId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    matchId = table.Column<int>(type: "INTEGER", nullable: false),
                    score = table.Column<string>(type: "TEXT", nullable: false),
                    temps = table.Column<int>(type: "INTEGER", nullable: false),
                    joueur = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_But", x => x.butId);
                    table.ForeignKey(
                        name: "FK_But_Match_matchId",
                        column: x => x.matchId,
                        principalTable: "Match",
                        principalColumn: "matchId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_But_matchId",
                table: "But",
                column: "matchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "But");

            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
