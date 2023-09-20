using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieWatchlist.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedAndLastUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            @"
                CREATE FUNCTION ""Update_LastUpdated_Function""() RETURNS TRIGGER LANGUAGE PLPGSQL AS $$
                BEGIN
                    NEW.""LastUpdated"" := now();
                    RETURN NEW;
                END;
                $$;

                CREATE TRIGGER ""UpdateLastUpdated""
                    BEFORE INSERT OR UPDATE
                    ON ""Movies"".""Movies""
                    FOR EACH ROW
                    EXECUTE FUNCTION ""Update_LastUpdated_Function""();

                CREATE TRIGGER ""UpdateLastUpdated""
                    BEFORE INSERT OR UPDATE
                    ON ""Watchlists"".""Watchlists""
                    FOR EACH ROW
                    EXECUTE FUNCTION ""Update_LastUpdated_Function""();

                CREATE TRIGGER ""UpdateLastUpdated""
                    BEFORE INSERT OR UPDATE
                    ON ""Watchlists"".""WatchlistsMovies""
                    FOR EACH ROW
                    EXECUTE FUNCTION ""Update_LastUpdated_Function""();
            ");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                schema: "Watchlists",
                table: "WatchlistsMovies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdated",
                schema: "Watchlists",
                table: "WatchlistsMovies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                schema: "Watchlists",
                table: "Watchlists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdated",
                schema: "Watchlists",
                table: "Watchlists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                schema: "Movies",
                table: "Movies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdated",
                schema: "Movies",
                table: "Movies",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Watchlists",
                table: "WatchlistsMovies");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                schema: "Watchlists",
                table: "WatchlistsMovies");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Watchlists",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                schema: "Watchlists",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Movies",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                schema: "Movies",
                table: "Movies");

            migrationBuilder.Sql(
            @"
                DROP FUNCTION ""Update_LastUpdated_Function""();

                DROP TRIGGER ""UpdateLastUpdated"" ON ""Movies"".""Movies"";

                DROP TRIGGER ""UpdateLastUpdated"" ON ""Watchlists"".""Watchlists"";

                DROP TRIGGER ""UpdateLastUpdated"" ON ""Watchlists"".""WatchlistsMovies"";
            ");
        }
    }
}
