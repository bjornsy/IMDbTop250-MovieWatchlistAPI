using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWatchlist.Infrastructure.Migrations
{
    public partial class AddWatchlistSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Watchlists");

            migrationBuilder.CreateTable(
                name: "Watchlists",
                schema: "Watchlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WatchlistsMovies",
                schema: "Watchlists",
                columns: table => new
                {
                    WatchlistId = table.Column<Guid>(type: "uuid", nullable: false),
                    MovieId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Watched = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistsMovies", x => new { x.WatchlistId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_WatchlistsMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalSchema: "Movies",
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchlistsMovies_Watchlists_WatchlistId",
                        column: x => x.WatchlistId,
                        principalSchema: "Watchlists",
                        principalTable: "Watchlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistsMovies_MovieId",
                schema: "Watchlists",
                table: "WatchlistsMovies",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchlistsMovies",
                schema: "Watchlists");

            migrationBuilder.DropTable(
                name: "Watchlists",
                schema: "Watchlists");
        }
    }
}
