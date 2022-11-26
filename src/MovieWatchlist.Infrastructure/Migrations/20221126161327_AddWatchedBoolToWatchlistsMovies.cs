using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWatchlist.Infrastructure.Migrations
{
    public partial class AddWatchedBoolToWatchlistsMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Watched",
                schema: "Watchlists",
                table: "WatchlistsMovies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Watched",
                schema: "Watchlists",
                table: "WatchlistsMovies");
        }
    }
}
