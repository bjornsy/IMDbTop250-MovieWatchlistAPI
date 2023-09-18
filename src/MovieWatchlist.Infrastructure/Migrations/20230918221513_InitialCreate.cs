using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieWatchlist.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Movies");

            migrationBuilder.EnsureSchema(
                name: "Watchlists");

            migrationBuilder.CreateTable(
                name: "Movies",
                schema: "Movies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Ranking = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

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
                    Watched = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistsMovies", x => new { x.MovieId, x.WatchlistId });
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

            migrationBuilder.InsertData(
                schema: "Movies",
                table: "Movies",
                columns: new[] { "Id", "Ranking", "Rating", "Title" },
                values: new object[,]
                {
                    { "0012349", 131, 8.2m, "The Kid (1921)" },
                    { "0015324", 198, 8.2m, "Sherlock Jr. (1924)" },
                    { "0015864", 183, 8.1m, "The Gold Rush (1925)" },
                    { "0017136", 117, 8.3m, "Metropolis (1927)" },
                    { "0017925", 191, 8.1m, "The General (1926)" },
                    { "0019254", 214, 8.1m, "La Passion de Jeanne d'Arc (1928)" },
                    { "0021749", 54, 8.5m, "City Lights (1931)" },
                    { "0022100", 100, 8.3m, "M (1931)" },
                    { "0025316", 243, 8.1m, "It Happened One Night (1934)" },
                    { "0027977", 49, 8.5m, "Modern Times (1936)" },
                    { "0031381", 158, 8.2m, "Gone with the Wind (1939)" },
                    { "0031679", 201, 8.1m, "Mr. Smith Goes to Washington (1939)" },
                    { "0032138", 226, 8.1m, "The Wizard of Oz (1939)" },
                    { "0032551", 234, 8.1m, "The Grapes of Wrath (1940)" },
                    { "0032553", 65, 8.4m, "The Great Dictator (1940)" },
                    { "0032976", 239, 8.1m, "Rebecca (1940)" },
                    { "0033467", 99, 8.3m, "Citizen Kane (1941)" },
                    { "0034583", 47, 8.5m, "Casablanca (1942)" },
                    { "0035446", 231, 8.2m, "To Be or Not to Be (1942)" },
                    { "0036775", 104, 8.3m, "Double Indemnity (1944)" },
                    { "0036868", 224, 8.1m, "The Best Years of Our Lives (1946)" },
                    { "0038650", 21, 8.6m, "It's a Wonderful Life (1946)" },
                    { "0040522", 120, 8.3m, "Ladri di biciclette (1948)" },
                    { "0040897", 147, 8.2m, "The Treasure of the Sierra Madre (1948)" },
                    { "0041959", 196, 8.1m, "The Third Man (1949)" },
                    { "0042192", 135, 8.2m, "All About Eve (1950)" },
                    { "0042876", 154, 8.2m, "Rashômon (1950)" },
                    { "0043014", 61, 8.4m, "Sunset Blvd. (1950)" },
                    { "0044741", 97, 8.3m, "Ikiru (1952)" },
                    { "0045152", 87, 8.3m, "Singin' in the Rain (1952)" },
                    { "0046268", 195, 8.2m, "Le Salaire de la peur (1953)" },
                    { "0046438", 210, 8.1m, "Tôkyô monogatari (1953)" },
                    { "0046912", 160, 8.2m, "Dial M for Murder (1954)" },
                    { "0047296", 188, 8.1m, "On the Waterfront (1954)" },
                    { "0047396", 52, 8.5m, "Rear Window (1954)" },
                    { "0047478", 22, 8.6m, "Shichinin no samurai (1954)" },
                    { "0048473", 242, 8.2m, "Pather Panchali (1955)" },
                    { "0050083", 5, 9.0m, "12 Angry Men (1957)" },
                    { "0050212", 169, 8.1m, "The Bridge on the River Kwai (1957)" },
                    { "0050825", 62, 8.4m, "Paths of Glory (1957)" },
                    { "0050976", 206, 8.1m, "Sjunde inseglet, Det (1957)" },
                    { "0050986", 194, 8.1m, "Smultronstället (1957)" },
                    { "0051201", 66, 8.4m, "Witness for the Prosecution (1957)" },
                    { "0052357", 103, 8.3m, "Vertigo (1958)" },
                    { "0052618", 185, 8.1m, "Ben-Hur (1959)" },
                    { "0053125", 102, 8.3m, "North by Northwest (1959)" },
                    { "0053198", 246, 8.1m, "Les Quatre cents coups (1959)" },
                    { "0053291", 130, 8.2m, "Some Like It Hot (1959)" },
                    { "0053604", 101, 8.3m, "The Apartment (1960)" },
                    { "0054215", 34, 8.5m, "Psycho (1960)" },
                    { "0055031", 136, 8.3m, "Judgment at Nuremberg (1961)" },
                    { "0055630", 148, 8.2m, "Yôjinbô (1961)" },
                    { "0056058", 46, 8.6m, "Seppuku (1962)" },
                    { "0056172", 98, 8.3m, "Lawrence of Arabia (1962)" },
                    { "0056592", 113, 8.3m, "To Kill a Mockingbird (1962)" },
                    { "0057012", 72, 8.4m, "Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb (1964)" },
                    { "0057115", 152, 8.2m, "The Great Escape (1963)" },
                    { "0057565", 86, 8.4m, "Tengoku to jigoku (1963)" },
                    { "0058946", 236, 8.1m, "La Battaglia di Algeri (1966)" },
                    { "0059578", 128, 8.2m, "Per qualche dollaro in più (1965)" },
                    { "0059742", 241, 8.1m, "The Sound of Music (1965)" },
                    { "0060196", 10, 8.8m, "Il buono, il brutto, il cattivo (1966)" },
                    { "0060827", 247, 8.1m, "Persona (1966)" },
                    { "0061512", 240, 8.1m, "Cool Hand Luke (1967)" },
                    { "0062622", 94, 8.3m, "2001: A Space Odyssey (1968)" },
                    { "0064116", 51, 8.5m, "C'era una volta il West (1968)" },
                    { "0066921", 108, 8.3m, "A Clockwork Orange (1971)" },
                    { "0068646", 2, 9.2m, "The Godfather (1972)" },
                    { "0070047", 228, 8.1m, "The Exorcist (1973)" },
                    { "0070735", 114, 8.3m, "The Sting (1973)" },
                    { "0071315", 157, 8.2m, "Chinatown (1974)" },
                    { "0071562", 4, 9.0m, "The Godfather: Part II (1974)" },
                    { "0071853", 151, 8.2m, "Monty Python and the Holy Grail (1975)" },
                    { "0072684", 187, 8.1m, "Barry Lyndon (1975)" },
                    { "0073195", 204, 8.1m, "Jaws (1975)" },
                    { "0073486", 18, 8.7m, "One Flew Over the Cuckoo's Nest (1975)" },
                    { "0074958", 222, 8.1m, "Network (1976)" },
                    { "0075148", 212, 8.1m, "Rocky (1976)" },
                    { "0075314", 123, 8.2m, "Taxi Driver (1976)" },
                    { "0076759", 29, 8.6m, "Star Wars (1977)" },
                    { "0077416", 193, 8.1m, "The Deer Hunter (1978)" },
                    { "0078748", 53, 8.5m, "Alien (1979)" },
                    { "0078788", 55, 8.4m, "Apocalypse Now (1979)" },
                    { "0079470", 248, 8.0m, "Life of Brian (1979)" },
                    { "0080678", 156, 8.2m, "The Elephant Man (1980)" },
                    { "0080684", 15, 8.7m, "The Empire Strikes Back (1980)" },
                    { "0081398", 163, 8.1m, "Raging Bull (1980)" },
                    { "0081505", 64, 8.4m, "The Shining (1980)" },
                    { "0082096", 77, 8.4m, "Das Boot (1981)" },
                    { "0082971", 58, 8.4m, "Raiders of the Lost Ark (1981)" },
                    { "0083658", 180, 8.1m, "Blade Runner (1982)" },
                    { "0084787", 153, 8.2m, "The Thing (1982)" },
                    { "0086190", 92, 8.3m, "Star Wars: Episode VI - Return of the Jedi (1983)" },
                    { "0086250", 106, 8.3m, "Scarface (1983)" },
                    { "0086879", 75, 8.4m, "Amadeus (1984)" },
                    { "0087843", 83, 8.3m, "Once Upon a Time in America (1984)" },
                    { "0088247", 218, 8.1m, "The Terminator (1984)" },
                    { "0088763", 31, 8.5m, "Back to the Future (1985)" },
                    { "0089881", 139, 8.2m, "Ran (1985)" },
                    { "0090605", 68, 8.4m, "Aliens (1986)" },
                    { "0091251", 91, 8.4m, "Idi i smotri (1985)" },
                    { "0091763", 216, 8.1m, "Platoon (1986)" },
                    { "0092005", 223, 8.1m, "Stand by Me (1986)" },
                    { "0093058", 107, 8.3m, "Full Metal Jacket (1987)" },
                    { "0095016", 118, 8.2m, "Die Hard (1988)" },
                    { "0095327", 45, 8.5m, "Hotaru no haka (1988)" },
                    { "0095765", 50, 8.5m, "Nuovo Cinema Paradiso (1988)" },
                    { "0096283", 175, 8.1m, "Tonari no Totoro (1988)" },
                    { "0097165", 200, 8.1m, "Dead Poets Society (1989)" },
                    { "0097576", 116, 8.2m, "Indiana Jones and the Last Crusade (1989)" },
                    { "0099348", 250, 8.0m, "Dances with Wolves (1990)" },
                    { "0099685", 17, 8.7m, "Goodfellas (1990)" },
                    { "0102926", 23, 8.6m, "The Silence of the Lambs (1991)" },
                    { "0103064", 30, 8.6m, "Terminator 2: Judgment Day (1991)" },
                    { "0103639", 249, 8.0m, "Aladdin (1992)" },
                    { "0105236", 96, 8.3m, "Reservoir Dogs (1992)" },
                    { "0105695", 144, 8.2m, "Unforgiven (1992)" },
                    { "0107048", 233, 8.0m, "Groundhog Day (1993)" },
                    { "0107207", 190, 8.1m, "In the Name of the Father (1993)" },
                    { "0107290", 145, 8.2m, "Jurassic Park (1993)" },
                    { "0108052", 6, 9.0m, "Schindler's List (1993)" },
                    { "0109830", 11, 8.8m, "Forrest Gump (1994)" },
                    { "0110357", 37, 8.5m, "The Lion King (1994)" },
                    { "0110413", 38, 8.5m, "Léon (1994)" },
                    { "0110912", 8, 8.9m, "Pulp Fiction (1994)" },
                    { "0111161", 1, 9.3m, "The Shawshank Redemption (1994)" },
                    { "0112471", 182, 8.1m, "Before Sunrise (1995)" },
                    { "0112573", 78, 8.3m, "Braveheart (1995)" },
                    { "0112641", 140, 8.2m, "Casino (1995)" },
                    { "0113247", 225, 8.1m, "La Haine (1995)" },
                    { "0113277", 110, 8.3m, "Heat (1995)" },
                    { "0114369", 20, 8.6m, "Se7en (1995)" },
                    { "0114709", 76, 8.3m, "Toy Story (1995)" },
                    { "0114814", 44, 8.5m, "The Usual Suspects (1995)" },
                    { "0116282", 171, 8.1m, "Fargo (1996)" },
                    { "0117951", 170, 8.1m, "Trainspotting (1996)" },
                    { "0118715", 209, 8.1m, "The Big Lebowski (1998)" },
                    { "0118799", 27, 8.6m, "La Vita è bella (1997)" },
                    { "0118849", 179, 8.2m, "Bacheha-Ye aseman (1997)" },
                    { "0119217", 82, 8.3m, "Good Will Hunting (1997)" },
                    { "0119488", 119, 8.2m, "L.A. Confidential (1997)" },
                    { "0119698", 81, 8.3m, "Mononoke-hime (1997)" },
                    { "0120382", 137, 8.2m, "The Truman Show (1998)" },
                    { "0120586", 39, 8.5m, "American History X (1998)" },
                    { "0120689", 28, 8.6m, "The Green Mile (1999)" },
                    { "0120735", 164, 8.1m, "Lock, Stock and Two Smoking Barrels (1998)" },
                    { "0120737", 9, 8.8m, "The Lord of the Rings: The Fellowship of the Ring (2001)" },
                    { "0120815", 25, 8.6m, "Saving Private Ryan (1998)" },
                    { "0129167", 244, 8.1m, "The Iron Giant (1999)" },
                    { "0133093", 16, 8.7m, "The Matrix (1999)" },
                    { "0137523", 12, 8.8m, "Fight Club (1999)" },
                    { "0167260", 7, 9.0m, "The Lord of the Rings: The Return of the King (2003)" },
                    { "0167261", 13, 8.8m, "The Lord of the Rings: The Two Towers (2002)" },
                    { "0167404", 143, 8.2m, "The Sixth Sense (1999)" },
                    { "0169547", 71, 8.3m, "American Beauty (1999)" },
                    { "0172495", 36, 8.5m, "Gladiator (2000)" },
                    { "0180093", 88, 8.3m, "Requiem for a Dream (2000)" },
                    { "0198781", 202, 8.1m, "Monsters, Inc. (2001)" },
                    { "0208092", 121, 8.2m, "Snatch. (2000)" },
                    { "0209144", 56, 8.4m, "Memento (2000)" },
                    { "0211915", 105, 8.3m, "Le Fabuleux destin d'Amélie Poulain (2001)" },
                    { "0245429", 32, 8.6m, "Sen to Chihiro no kamikakushi (2001)" },
                    { "0245712", 238, 8.1m, "Amores perros (2000)" },
                    { "0253474", 33, 8.5m, "The Pianist (2002)" },
                    { "0264464", 174, 8.1m, "Catch Me If You Can (2002)" },
                    { "0266543", 155, 8.2m, "Finding Nemo (2003)" },
                    { "0266697", 150, 8.2m, "Kill Bill: Vol. 1 (2003)" },
                    { "0268978", 146, 8.2m, "A Beautiful Mind (2001)" },
                    { "0317248", 26, 8.6m, "Cidade de Deus (2002)" },
                    { "0317705", 230, 8.0m, "The Incredibles (2004)" },
                    { "0325980", 229, 8.1m, "Pirates of the Caribbean: The Curse of the Black Pearl (2003)" },
                    { "0338013", 93, 8.3m, "Eternal Sunshine of the Spotless Mind (2004)" },
                    { "0347149", 159, 8.2m, "Hauru no ugoku shiro (2004)" },
                    { "0353969", 192, 8.1m, "Salinui chueok (2003)" },
                    { "0361748", 70, 8.3m, "Inglourious Basterds (2009)" },
                    { "0363163", 125, 8.2m, "Der Untergang (2004)" },
                    { "0364569", 73, 8.4m, "Oldeuboi (2003)" },
                    { "0372784", 129, 8.2m, "Batman Begins (2005)" },
                    { "0381681", 220, 8.1m, "Before Sunset (2004)" },
                    { "0382932", 211, 8.1m, "Ratatouille (2007)" },
                    { "0395169", 213, 8.1m, "Hotel Rwanda (2004)" },
                    { "0405094", 60, 8.4m, "Das Leben der Anderen (2006)" },
                    { "0405159", 176, 8.1m, "Million Dollar Baby (2004)" },
                    { "0407887", 40, 8.5m, "The Departed (2006)" },
                    { "0434409", 161, 8.2m, "V for Vendetta (2005)" },
                    { "0435761", 90, 8.3m, "Toy Story 3 (2010)" },
                    { "0457430", 142, 8.2m, "El Laberinto del fauno (2006)" },
                    { "0468569", 3, 9.0m, "The Dark Knight (2008)" },
                    { "0469494", 138, 8.2m, "There Will Be Blood (2007)" },
                    { "0476735", 237, 8.2m, "Babam Ve Oglum (2005)" },
                    { "0477348", 149, 8.2m, "No Country for Old Men (2007)" },
                    { "0482571", 42, 8.5m, "The Prestige (2006)" },
                    { "0758758", 227, 8.1m, "Into the Wild (2007)" },
                    { "0816692", 24, 8.7m, "Interstellar (2014)" },
                    { "0892769", 203, 8.1m, "How to Train Your Dragon (2010)" },
                    { "0910970", 59, 8.4m, "WALL·E (2008)" },
                    { "0978762", 205, 8.1m, "Mary and Max (2009)" },
                    { "0986264", 122, 8.3m, "Taare Zameen Par (2007)" },
                    { "0993846", 132, 8.2m, "The Wolf of Wall Street (2013)" },
                    { "10272386", 133, 8.2m, "The Father (2020)" },
                    { "1028532", 232, 8.1m, "Hachi: A Dog's Tale (2009)" },
                    { "1049413", 112, 8.3m, "Up (2009)" },
                    { "10872600", 162, 8.2m, "Spider-Man: No Way Home (2021)" },
                    { "1130884", 141, 8.2m, "Shutter Island (2010)" },
                    { "1187043", 85, 8.4m, "3 Idiots (2009)" },
                    { "1201607", 178, 8.1m, "Harry Potter and the Deathly Hallows: Part 2 (2011)" },
                    { "1205489", 173, 8.1m, "Gran Torino (2008)" },
                    { "1255953", 109, 8.3m, "Incendies (2010)" },
                    { "1291584", 172, 8.1m, "Warrior (2011)" },
                    { "1305806", 165, 8.2m, "El Secreto de sus ojos (2009)" },
                    { "1345836", 69, 8.4m, "The Dark Knight Rises (2012)" },
                    { "1375666", 14, 8.8m, "Inception (2010)" },
                    { "1392190", 199, 8.1m, "Mad Max: Fury Road (2015)" },
                    { "1392214", 167, 8.1m, "Prisoners (2013)" },
                    { "1454029", 245, 8.1m, "The Help (2011)" },
                    { "15097216", 219, 8.8m, "Jai Bhim (2021)" },
                    { "15398776", 43, 8.6m, "Oppenheimer (2023)" },
                    { "1675434", 48, 8.5m, "Intouchables (2011)" },
                    { "1745960", 127, 8.3m, "Top Gun: Maverick (2022)" },
                    { "1832382", 115, 8.3m, "Jodaeiye Nader az Simin (2011)" },
                    { "1853728", 57, 8.5m, "Django Unchained (2012)" },
                    { "1895587", 217, 8.1m, "Spotlight (2015)" },
                    { "1950186", 208, 8.1m, "Ford v Ferrari (2019)" },
                    { "1979320", 221, 8.1m, "Rush (2013)" },
                    { "2024544", 181, 8.1m, "12 Years a Slave (2013)" },
                    { "2096673", 166, 8.1m, "Inside Out (2015)" },
                    { "2106476", 95, 8.3m, "Jagten (2012)" },
                    { "2119532", 189, 8.1m, "Hacksaw Ridge (2016)" },
                    { "2267998", 186, 8.1m, "Gone Girl (2014)" },
                    { "2278388", 184, 8.1m, "The Grand Budapest Hotel (2014)" },
                    { "2380307", 74, 8.4m, "Coco (2017)" },
                    { "2582802", 41, 8.5m, "Whiplash (2014)" },
                    { "3011894", 197, 8.1m, "Relatos salvajes (2014)" },
                    { "3170832", 207, 8.1m, "Room (2015)" },
                    { "3315342", 215, 8.1m, "Logan (2017)" },
                    { "4016934", 235, 8.1m, "Ah-ga-ssi (2016)" },
                    { "4154756", 63, 8.4m, "Avengers: Infinity War (2018)" },
                    { "4154796", 79, 8.4m, "Avengers: Endgame (2019)" },
                    { "4633694", 67, 8.4m, "Spider-Man: Into the Spider-Verse (2018)" },
                    { "4729430", 177, 8.2m, "Klaus (2019)" },
                    { "5027774", 168, 8.1m, "Three Billboards Outside Ebbing, Missouri (2017)" },
                    { "5074352", 126, 8.3m, "Dangal (2016)" },
                    { "5311514", 84, 8.4m, "Kimi no na wa. (2016)" },
                    { "6751668", 35, 8.5m, "Gisaengchung (2019)" },
                    { "6966692", 134, 8.2m, "Green Book (2018)" },
                    { "7286456", 80, 8.4m, "Joker (2019)" },
                    { "8267604", 89, 8.4m, "Capharnaüm (2018)" },
                    { "8503618", 111, 8.3m, "Hamilton (2020)" },
                    { "8579674", 124, 8.2m, "1917 (2019)" },
                    { "9362722", 19, 8.7m, "Spider-Man: Across the Spider-Verse (2023)" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistsMovies_WatchlistId",
                schema: "Watchlists",
                table: "WatchlistsMovies",
                column: "WatchlistId")
                .Annotation("Npgsql:IndexInclude", new[] { "MovieId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchlistsMovies",
                schema: "Watchlists");

            migrationBuilder.DropTable(
                name: "Movies",
                schema: "Movies");

            migrationBuilder.DropTable(
                name: "Watchlists",
                schema: "Watchlists");
        }
    }
}
