# Movie Watchlist API for IMDb Top 250

API to view the top 250 rated movies from IMDb, and save movies that you want to watch and set as watched.

## Description

Written in .NET and using a Postgres SQL database, it provides endpoints to manage watchlists.

### Basic usage

* `GET` the latest top 250 list from `/Movies`
* Create a watchlist with a name and array of movie Ids (`POST` to `/Watchlists`)
* After creation, you can add/remove movies, set movies as "watched" or delete/rename the watchlist

### Top 250 Movie update process

The list of movies is obtained from <http://top250.info/>, under the Top 250 section which is updated each day from IMDb itself.

If the Movies table is not fully populated, the [DbInitializer](src/MovieWatchlist.Infrastructure/Data/DbInitializer.cs) will add movies from a [pre-populated CSV file](src/MovieWatchlist.Api/Top250MoviesSeed.csv) on application start (in case the website is down). This file can be updated by the separate [Top250Scraper console application](Top250Scraper/Program.cs) if desired.

Within the `GET Movies` endpoint, if the cache is empty the [Top250InfoService](src/MovieWatchlist.Api/Services/Top250InfoService.cs) will scrape the list of movies from the page of the current day, then [Top250MoviesDatabaseUpdateService](src/MovieWatchlist.Api/Services/Top250MoviesDatabaseUpdateService.cs) will update the table in the database.

![sequence diagram](/docs/Top250MoviesSequenceDiagram_PlantText.png)

## Getting Started

### Dependencies

* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* [.NET 7 (for development)](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

### How to run

* After cloning, run `docker compose build` in the root folder, then `docker compose up`
* The Swagger page will be available at <http://localhost:8080/swagger/index.html>
* The healthcheck is available at <http://localhost:8080/_health>

### How to develop

* After `docker compose build`, start the Postgres container with a volume: `docker compose start moviewatchlist-db`
* After starting the program in your IDE, the Swagger page is available at <https://localhost:7069/swagger/index.html>
* To check data in the database container, use the following commands:
  * Get running containers `docker container list`
  * `docker exec -it <database container ID> bash`
  * Select movies in the table:

    ```
    psql --username postgres --password
    \l
    \c moviewatchlist
    \dt *.*
    SELECT * FROM "Movies"."Movies";
    ```

* If making schema changes, the following Entity Framework commands might help
    * `dotnet tool install --global dotnet-ef`
    * `dotnet ef --startup-project ../MovieWatchlist.Api/ migrations add <migration name>`
    * `dotnet ef --startup-project ../MovieWatchlist.Api/ database update`

## Future updates

Planned updates include:

* .NET 8
* [Use .NET 8 Identity API for authentication](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0?view=aspnetcore-7.0#identity-api-endpoints)
* [Distributed Redis output cache](https://learn.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-8.0?view=aspnetcore-7.0#redis-based-output-caching)
* [Use ExecuteDeleteAsync](https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-7.0/whatsnew#executeupdate-and-executedelete-bulk-updates)
* Change to NSubstitute from Moq
* Implement HATEOAS
