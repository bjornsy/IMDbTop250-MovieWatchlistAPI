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

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0012349",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0015324",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0015864",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0017136",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0017925",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0019254",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0021749",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0022100",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0025316",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0027977",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0031381",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0031679",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0032138",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0032551",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0032553",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0032976",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0033467",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0034583",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0035446",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0036775",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0036868",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0038650",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0040522",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0040897",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0041959",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0042192",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0042876",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0043014",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0044741",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0045152",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0046268",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0046438",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0046912",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0047296",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0047396",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0047478",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0048473",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0050083",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0050212",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0050825",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0050976",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0050986",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0051201",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0052357",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0052618",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0053125",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0053198",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0053291",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0053604",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0054215",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0055031",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0055630",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0056058",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0056172",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0056592",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0057012",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0057115",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0057565",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0058946",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0059578",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0059742",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0060196",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0060827",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0061512",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0062622",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0064116",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0066921",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0068646",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0070047",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0070735",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0071315",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0071562",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0071853",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0072684",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0073195",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0073486",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0074958",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0075148",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0075314",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0076759",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0077416",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0078748",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0078788",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0079470",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0080678",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0080684",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0081398",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0081505",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0082096",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0082971",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0083658",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0084787",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0086190",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0086250",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0086879",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0087843",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0088247",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0088763",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0089881",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0090605",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0091251",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0091763",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0092005",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0093058",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0095016",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0095327",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0095765",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0096283",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0097165",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0097576",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0099348",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0099685",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0102926",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0103064",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0103639",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0105236",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0105695",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0107048",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0107207",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0107290",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0108052",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0109830",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0110357",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0110413",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0110912",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0111161",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0112471",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0112573",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0112641",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0113247",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0113277",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0114369",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0114709",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0114814",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0116282",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0117951",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0118715",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0118799",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0118849",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0119217",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0119488",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0119698",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0120382",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0120586",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0120689",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0120735",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0120737",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0120815",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0129167",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0133093",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0137523",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0167260",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0167261",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0167404",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0169547",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0172495",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0180093",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0198781",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0208092",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0209144",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0211915",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0245429",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0245712",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0253474",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0264464",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0266543",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0266697",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0268978",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0317248",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0317705",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0325980",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0338013",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0347149",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0353969",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0361748",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0363163",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0364569",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0372784",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0381681",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0382932",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0395169",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0405094",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0405159",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0407887",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0434409",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0435761",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0457430",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0468569",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0469494",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0476735",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0477348",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0482571",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0758758",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0816692",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0892769",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0910970",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0978762",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0986264",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "0993846",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "10272386",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1028532",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1049413",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "10872600",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1130884",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1187043",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1201607",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1205489",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1255953",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1291584",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1305806",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1345836",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1375666",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1392190",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1392214",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1454029",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "15097216",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "15398776",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1675434",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1745960",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1832382",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1853728",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1895587",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1950186",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "1979320",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2024544",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2096673",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2106476",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2119532",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2267998",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2278388",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2380307",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "2582802",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "3011894",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "3170832",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "3315342",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "4016934",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "4154756",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "4154796",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "4633694",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "4729430",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "5027774",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "5074352",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "5311514",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "6751668",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "6966692",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "7286456",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "8267604",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "8503618",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "8579674",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);

            //migrationBuilder.UpdateData(
            //    schema: "Movies",
            //    table: "Movies",
            //    keyColumn: "Id",
            //    keyValue: "9362722",
            //    column: "LastUpdated",
            //    value: DateTimeOffset.UtcNow);
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
