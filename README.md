[![Unit tests with GitHub Actions](https://github.com/bjornsy/IMDbTop250-MovieWatchlistAPI/actions/workflows/dotnet-test-unit.yml/badge.svg?branch=main)](https://github.com/bjornsy/IMDbTop250-MovieWatchlistAPI/actions/workflows/dotnet-test-unit.yml)

# Movie Watchlist API for IMDb Top 250

API to view the top 250 rated movies from IMDb, and save movies that you want to watch and set as watched.

## Description

Written in .NET and using a Postgres SQL database, it provides endpoints to manage watchlists.

### Basic usage

* `GET` the latest top 250 list from `/Movies/top250`
* Create a watchlist with a name and array of movie Ids (`POST` to `/Watchlists`)
* After creation, you can add/remove movies, set movies as "watched" or delete/rename the watchlist

### Top 250 Movie update process

The list of movies is obtained from <http://top250.info/>, under the Top 250 section which is updated each day from IMDb itself.

In case the website is down, as part of the Entity Framework migration, movie data is seeded from a [pre-populated CSV file](src/MovieWatchlist.Api/Top250MoviesSeed.csv). This file can be updated by the separate [Top250Scraper console application](Top250Scraper/Program.cs) if desired.

Within the `GET Movies` endpoint, the [Top250InfoService](src/MovieWatchlist.Api/Services/Top250InfoService.cs) will scrape the list of movies from the page of the current day, then [Top250MoviesDatabaseUpdateService](src/MovieWatchlist.Api/Services/Top250MoviesDatabaseUpdateService.cs) will update the table in the database.

There is a distributed Redis output cache in place, using a container and the [.NET built-in support](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/output?view=aspnetcore-8.0#redis-cache).

![sequence diagram](/docs/Top250MoviesSequenceDiagram_PlantText.png)

## Getting Started

### Dependencies

* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [.NET 8 (for development)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### How to run

* After cloning, run `docker compose up` in the root folder
* The Swagger page will be available at <http://localhost:8080/swagger/index.html>
* The healthcheck is available at <http://localhost:8080/_health>

### How to develop locally

* Ensure the docker volume is up (like if you've done `docker compose up`, or after `docker compose build` then starting the Postgres container independently: `docker compose start moviewatchlist-db`)
* After starting the program in your IDE, the Swagger page is available at <https://localhost:7069/swagger/index.html>
* To check data in the database container, use the following commands:
  * Get running containers `docker container list`
  * `docker exec -it <database container ID> bash`
  * Select movies in the table:

    ```
    psql --username postgres
    \l
    \c moviewatchlist
    \dt *.*
    SELECT * FROM "Movies"."Movies";
    ```

* If making schema changes, the following Entity Framework commands might help
    * `dotnet tool install --global dotnet-ef`
    * `dotnet ef --startup-project ../MovieWatchlist.Api/ migrations add <migration name>`
    * `dotnet ef --startup-project ../MovieWatchlist.Api/ database update`

  The migrations are applied during the `compose` step, with `init.sql`. This file can be regenerated after adding a migration with `dotnet ef --startup-project ../MovieWatchlist.Api/ migrations script --output ./init.sql` (this mimics [the recommended way](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/applying?tabs=dotnet-core-cli#sql-scripts) of applying migrations, rather than executing on application start).

## Future updates

Planned updates include:

* [Use .NET 8 Identity API for authentication](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0?view=aspnetcore-7.0#identity-api-endpoints)
* [Use ExecuteDeleteAsync](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew#executeupdate-and-executedelete-bulk-updates)
* Change to NSubstitute from Moq
* Implement HATEOAS
* Run integration tests in GitHub Actions (not just unit tests) once caching of images within the compose file is supported
