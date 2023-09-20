CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Movies') THEN
        CREATE SCHEMA "Movies";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'Watchlists') THEN
        CREATE SCHEMA "Watchlists";
    END IF;
END $EF$;

CREATE TABLE "Movies"."Movies" (
    "Id" text NOT NULL,
    "Ranking" integer NULL,
    "Title" text NOT NULL,
    "Rating" numeric(2,1) NOT NULL,
    CONSTRAINT "PK_Movies" PRIMARY KEY ("Id")
);

CREATE TABLE "Watchlists"."Watchlists" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_Watchlists" PRIMARY KEY ("Id")
);

CREATE TABLE "Watchlists"."WatchlistsMovies" (
    "WatchlistId" uuid NOT NULL,
    "MovieId" text NOT NULL,
    "Watched" boolean NOT NULL,
    CONSTRAINT "PK_WatchlistsMovies" PRIMARY KEY ("MovieId", "WatchlistId"),
    CONSTRAINT "FK_WatchlistsMovies_Movies_MovieId" FOREIGN KEY ("MovieId") REFERENCES "Movies"."Movies" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_WatchlistsMovies_Watchlists_WatchlistId" FOREIGN KEY ("WatchlistId") REFERENCES "Watchlists"."Watchlists" ("Id") ON DELETE CASCADE
);

INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0012349', 131, 8.2, 'The Kid (1921)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0015324', 198, 8.2, 'Sherlock Jr. (1924)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0015864', 183, 8.1, 'The Gold Rush (1925)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0017136', 117, 8.3, 'Metropolis (1927)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0017925', 191, 8.1, 'The General (1926)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0019254', 214, 8.1, 'La Passion de Jeanne d''Arc (1928)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0021749', 54, 8.5, 'City Lights (1931)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0022100', 100, 8.3, 'M (1931)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0025316', 243, 8.1, 'It Happened One Night (1934)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0027977', 49, 8.5, 'Modern Times (1936)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0031381', 158, 8.2, 'Gone with the Wind (1939)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0031679', 201, 8.1, 'Mr. Smith Goes to Washington (1939)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0032138', 226, 8.1, 'The Wizard of Oz (1939)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0032551', 234, 8.1, 'The Grapes of Wrath (1940)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0032553', 65, 8.4, 'The Great Dictator (1940)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0032976', 239, 8.1, 'Rebecca (1940)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0033467', 99, 8.3, 'Citizen Kane (1941)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0034583', 47, 8.5, 'Casablanca (1942)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0035446', 231, 8.2, 'To Be or Not to Be (1942)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0036775', 104, 8.3, 'Double Indemnity (1944)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0036868', 224, 8.1, 'The Best Years of Our Lives (1946)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0038650', 21, 8.6, 'It''s a Wonderful Life (1946)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0040522', 120, 8.3, 'Ladri di biciclette (1948)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0040897', 147, 8.2, 'The Treasure of the Sierra Madre (1948)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0041959', 196, 8.1, 'The Third Man (1949)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0042192', 135, 8.2, 'All About Eve (1950)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0042876', 154, 8.2, 'Rashômon (1950)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0043014', 61, 8.4, 'Sunset Blvd. (1950)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0044741', 97, 8.3, 'Ikiru (1952)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0045152', 87, 8.3, 'Singin'' in the Rain (1952)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0046268', 195, 8.2, 'Le Salaire de la peur (1953)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0046438', 210, 8.1, 'Tôkyô monogatari (1953)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0046912', 160, 8.2, 'Dial M for Murder (1954)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0047296', 188, 8.1, 'On the Waterfront (1954)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0047396', 52, 8.5, 'Rear Window (1954)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0047478', 22, 8.6, 'Shichinin no samurai (1954)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0048473', 242, 8.2, 'Pather Panchali (1955)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0050083', 5, 9.0, '12 Angry Men (1957)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0050212', 169, 8.1, 'The Bridge on the River Kwai (1957)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0050825', 62, 8.4, 'Paths of Glory (1957)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0050976', 206, 8.1, 'Sjunde inseglet, Det (1957)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0050986', 194, 8.1, 'Smultronstället (1957)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0051201', 66, 8.4, 'Witness for the Prosecution (1957)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0052357', 103, 8.3, 'Vertigo (1958)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0052618', 185, 8.1, 'Ben-Hur (1959)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0053125', 102, 8.3, 'North by Northwest (1959)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0053198', 246, 8.1, 'Les Quatre cents coups (1959)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0053291', 130, 8.2, 'Some Like It Hot (1959)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0053604', 101, 8.3, 'The Apartment (1960)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0054215', 34, 8.5, 'Psycho (1960)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0055031', 136, 8.3, 'Judgment at Nuremberg (1961)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0055630', 148, 8.2, 'Yôjinbô (1961)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0056058', 46, 8.6, 'Seppuku (1962)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0056172', 98, 8.3, 'Lawrence of Arabia (1962)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0056592', 113, 8.3, 'To Kill a Mockingbird (1962)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0057012', 72, 8.4, 'Dr. Strangelove or: How I Learned to Stop Worrying and Love the Bomb (1964)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0057115', 152, 8.2, 'The Great Escape (1963)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0057565', 86, 8.4, 'Tengoku to jigoku (1963)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0058946', 236, 8.1, 'La Battaglia di Algeri (1966)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0059578', 128, 8.2, 'Per qualche dollaro in più (1965)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0059742', 241, 8.1, 'The Sound of Music (1965)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0060196', 10, 8.8, 'Il buono, il brutto, il cattivo (1966)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0060827', 247, 8.1, 'Persona (1966)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0061512', 240, 8.1, 'Cool Hand Luke (1967)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0062622', 94, 8.3, '2001: A Space Odyssey (1968)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0064116', 51, 8.5, 'C''era una volta il West (1968)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0066921', 108, 8.3, 'A Clockwork Orange (1971)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0068646', 2, 9.2, 'The Godfather (1972)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0070047', 228, 8.1, 'The Exorcist (1973)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0070735', 114, 8.3, 'The Sting (1973)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0071315', 157, 8.2, 'Chinatown (1974)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0071562', 4, 9.0, 'The Godfather: Part II (1974)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0071853', 151, 8.2, 'Monty Python and the Holy Grail (1975)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0072684', 187, 8.1, 'Barry Lyndon (1975)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0073195', 204, 8.1, 'Jaws (1975)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0073486', 18, 8.7, 'One Flew Over the Cuckoo''s Nest (1975)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0074958', 222, 8.1, 'Network (1976)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0075148', 212, 8.1, 'Rocky (1976)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0075314', 123, 8.2, 'Taxi Driver (1976)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0076759', 29, 8.6, 'Star Wars (1977)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0077416', 193, 8.1, 'The Deer Hunter (1978)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0078748', 53, 8.5, 'Alien (1979)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0078788', 55, 8.4, 'Apocalypse Now (1979)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0079470', 248, 8.0, 'Life of Brian (1979)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0080678', 156, 8.2, 'The Elephant Man (1980)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0080684', 15, 8.7, 'The Empire Strikes Back (1980)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0081398', 163, 8.1, 'Raging Bull (1980)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0081505', 64, 8.4, 'The Shining (1980)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0082096', 77, 8.4, 'Das Boot (1981)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0082971', 58, 8.4, 'Raiders of the Lost Ark (1981)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0083658', 180, 8.1, 'Blade Runner (1982)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0084787', 153, 8.2, 'The Thing (1982)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0086190', 92, 8.3, 'Star Wars: Episode VI - Return of the Jedi (1983)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0086250', 106, 8.3, 'Scarface (1983)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0086879', 75, 8.4, 'Amadeus (1984)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0087843', 83, 8.3, 'Once Upon a Time in America (1984)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0088247', 218, 8.1, 'The Terminator (1984)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0088763', 31, 8.5, 'Back to the Future (1985)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0089881', 139, 8.2, 'Ran (1985)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0090605', 68, 8.4, 'Aliens (1986)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0091251', 91, 8.4, 'Idi i smotri (1985)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0091763', 216, 8.1, 'Platoon (1986)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0092005', 223, 8.1, 'Stand by Me (1986)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0093058', 107, 8.3, 'Full Metal Jacket (1987)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0095016', 118, 8.2, 'Die Hard (1988)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0095327', 45, 8.5, 'Hotaru no haka (1988)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0095765', 50, 8.5, 'Nuovo Cinema Paradiso (1988)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0096283', 175, 8.1, 'Tonari no Totoro (1988)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0097165', 200, 8.1, 'Dead Poets Society (1989)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0097576', 116, 8.2, 'Indiana Jones and the Last Crusade (1989)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0099348', 250, 8.0, 'Dances with Wolves (1990)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0099685', 17, 8.7, 'Goodfellas (1990)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0102926', 23, 8.6, 'The Silence of the Lambs (1991)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0103064', 30, 8.6, 'Terminator 2: Judgment Day (1991)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0103639', 249, 8.0, 'Aladdin (1992)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0105236', 96, 8.3, 'Reservoir Dogs (1992)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0105695', 144, 8.2, 'Unforgiven (1992)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0107048', 233, 8.0, 'Groundhog Day (1993)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0107207', 190, 8.1, 'In the Name of the Father (1993)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0107290', 145, 8.2, 'Jurassic Park (1993)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0108052', 6, 9.0, 'Schindler''s List (1993)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0109830', 11, 8.8, 'Forrest Gump (1994)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0110357', 37, 8.5, 'The Lion King (1994)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0110413', 38, 8.5, 'Léon (1994)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0110912', 8, 8.9, 'Pulp Fiction (1994)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0111161', 1, 9.3, 'The Shawshank Redemption (1994)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0112471', 182, 8.1, 'Before Sunrise (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0112573', 78, 8.3, 'Braveheart (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0112641', 140, 8.2, 'Casino (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0113247', 225, 8.1, 'La Haine (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0113277', 110, 8.3, 'Heat (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0114369', 20, 8.6, 'Se7en (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0114709', 76, 8.3, 'Toy Story (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0114814', 44, 8.5, 'The Usual Suspects (1995)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0116282', 171, 8.1, 'Fargo (1996)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0117951', 170, 8.1, 'Trainspotting (1996)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0118715', 209, 8.1, 'The Big Lebowski (1998)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0118799', 27, 8.6, 'La Vita è bella (1997)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0118849', 179, 8.2, 'Bacheha-Ye aseman (1997)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0119217', 82, 8.3, 'Good Will Hunting (1997)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0119488', 119, 8.2, 'L.A. Confidential (1997)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0119698', 81, 8.3, 'Mononoke-hime (1997)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0120382', 137, 8.2, 'The Truman Show (1998)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0120586', 39, 8.5, 'American History X (1998)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0120689', 28, 8.6, 'The Green Mile (1999)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0120735', 164, 8.1, 'Lock, Stock and Two Smoking Barrels (1998)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0120737', 9, 8.8, 'The Lord of the Rings: The Fellowship of the Ring (2001)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0120815', 25, 8.6, 'Saving Private Ryan (1998)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0129167', 244, 8.1, 'The Iron Giant (1999)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0133093', 16, 8.7, 'The Matrix (1999)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0137523', 12, 8.8, 'Fight Club (1999)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0167260', 7, 9.0, 'The Lord of the Rings: The Return of the King (2003)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0167261', 13, 8.8, 'The Lord of the Rings: The Two Towers (2002)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0167404', 143, 8.2, 'The Sixth Sense (1999)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0169547', 71, 8.3, 'American Beauty (1999)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0172495', 36, 8.5, 'Gladiator (2000)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0180093', 88, 8.3, 'Requiem for a Dream (2000)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0198781', 202, 8.1, 'Monsters, Inc. (2001)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0208092', 121, 8.2, 'Snatch. (2000)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0209144', 56, 8.4, 'Memento (2000)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0211915', 105, 8.3, 'Le Fabuleux destin d''Amélie Poulain (2001)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0245429', 32, 8.6, 'Sen to Chihiro no kamikakushi (2001)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0245712', 238, 8.1, 'Amores perros (2000)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0253474', 33, 8.5, 'The Pianist (2002)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0264464', 174, 8.1, 'Catch Me If You Can (2002)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0266543', 155, 8.2, 'Finding Nemo (2003)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0266697', 150, 8.2, 'Kill Bill: Vol. 1 (2003)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0268978', 146, 8.2, 'A Beautiful Mind (2001)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0317248', 26, 8.6, 'Cidade de Deus (2002)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0317705', 230, 8.0, 'The Incredibles (2004)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0325980', 229, 8.1, 'Pirates of the Caribbean: The Curse of the Black Pearl (2003)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0338013', 93, 8.3, 'Eternal Sunshine of the Spotless Mind (2004)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0347149', 159, 8.2, 'Hauru no ugoku shiro (2004)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0353969', 192, 8.1, 'Salinui chueok (2003)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0361748', 70, 8.3, 'Inglourious Basterds (2009)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0363163', 125, 8.2, 'Der Untergang (2004)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0364569', 73, 8.4, 'Oldeuboi (2003)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0372784', 129, 8.2, 'Batman Begins (2005)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0381681', 220, 8.1, 'Before Sunset (2004)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0382932', 211, 8.1, 'Ratatouille (2007)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0395169', 213, 8.1, 'Hotel Rwanda (2004)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0405094', 60, 8.4, 'Das Leben der Anderen (2006)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0405159', 176, 8.1, 'Million Dollar Baby (2004)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0407887', 40, 8.5, 'The Departed (2006)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0434409', 161, 8.2, 'V for Vendetta (2005)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0435761', 90, 8.3, 'Toy Story 3 (2010)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0457430', 142, 8.2, 'El Laberinto del fauno (2006)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0468569', 3, 9.0, 'The Dark Knight (2008)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0469494', 138, 8.2, 'There Will Be Blood (2007)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0476735', 237, 8.2, 'Babam Ve Oglum (2005)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0477348', 149, 8.2, 'No Country for Old Men (2007)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0482571', 42, 8.5, 'The Prestige (2006)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0758758', 227, 8.1, 'Into the Wild (2007)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0816692', 24, 8.7, 'Interstellar (2014)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0892769', 203, 8.1, 'How to Train Your Dragon (2010)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0910970', 59, 8.4, 'WALL·E (2008)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0978762', 205, 8.1, 'Mary and Max (2009)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0986264', 122, 8.3, 'Taare Zameen Par (2007)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('0993846', 132, 8.2, 'The Wolf of Wall Street (2013)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('10272386', 133, 8.2, 'The Father (2020)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1028532', 232, 8.1, 'Hachi: A Dog''s Tale (2009)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1049413', 112, 8.3, 'Up (2009)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('10872600', 162, 8.2, 'Spider-Man: No Way Home (2021)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1130884', 141, 8.2, 'Shutter Island (2010)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1187043', 85, 8.4, '3 Idiots (2009)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1201607', 178, 8.1, 'Harry Potter and the Deathly Hallows: Part 2 (2011)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1205489', 173, 8.1, 'Gran Torino (2008)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1255953', 109, 8.3, 'Incendies (2010)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1291584', 172, 8.1, 'Warrior (2011)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1305806', 165, 8.2, 'El Secreto de sus ojos (2009)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1345836', 69, 8.4, 'The Dark Knight Rises (2012)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1375666', 14, 8.8, 'Inception (2010)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1392190', 199, 8.1, 'Mad Max: Fury Road (2015)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1392214', 167, 8.1, 'Prisoners (2013)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1454029', 245, 8.1, 'The Help (2011)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('15097216', 219, 8.8, 'Jai Bhim (2021)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('15398776', 43, 8.6, 'Oppenheimer (2023)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1675434', 48, 8.5, 'Intouchables (2011)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1745960', 127, 8.3, 'Top Gun: Maverick (2022)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1832382', 115, 8.3, 'Jodaeiye Nader az Simin (2011)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1853728', 57, 8.5, 'Django Unchained (2012)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1895587', 217, 8.1, 'Spotlight (2015)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1950186', 208, 8.1, 'Ford v Ferrari (2019)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('1979320', 221, 8.1, 'Rush (2013)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2024544', 181, 8.1, '12 Years a Slave (2013)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2096673', 166, 8.1, 'Inside Out (2015)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2106476', 95, 8.3, 'Jagten (2012)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2119532', 189, 8.1, 'Hacksaw Ridge (2016)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2267998', 186, 8.1, 'Gone Girl (2014)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2278388', 184, 8.1, 'The Grand Budapest Hotel (2014)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2380307', 74, 8.4, 'Coco (2017)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('2582802', 41, 8.5, 'Whiplash (2014)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('3011894', 197, 8.1, 'Relatos salvajes (2014)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('3170832', 207, 8.1, 'Room (2015)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('3315342', 215, 8.1, 'Logan (2017)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('4016934', 235, 8.1, 'Ah-ga-ssi (2016)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('4154756', 63, 8.4, 'Avengers: Infinity War (2018)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('4154796', 79, 8.4, 'Avengers: Endgame (2019)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('4633694', 67, 8.4, 'Spider-Man: Into the Spider-Verse (2018)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('4729430', 177, 8.2, 'Klaus (2019)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('5027774', 168, 8.1, 'Three Billboards Outside Ebbing, Missouri (2017)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('5074352', 126, 8.3, 'Dangal (2016)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('5311514', 84, 8.4, 'Kimi no na wa. (2016)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('6751668', 35, 8.5, 'Gisaengchung (2019)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('6966692', 134, 8.2, 'Green Book (2018)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('7286456', 80, 8.4, 'Joker (2019)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('8267604', 89, 8.4, 'Capharnaüm (2018)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('8503618', 111, 8.3, 'Hamilton (2020)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('8579674', 124, 8.2, '1917 (2019)');
INSERT INTO "Movies"."Movies" ("Id", "Ranking", "Rating", "Title")
VALUES ('9362722', 19, 8.7, 'Spider-Man: Across the Spider-Verse (2023)');

CREATE INDEX "IX_WatchlistsMovies_WatchlistId" ON "Watchlists"."WatchlistsMovies" ("WatchlistId") INCLUDE ("MovieId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230918221513_InitialCreate', '7.0.8');

COMMIT;

START TRANSACTION;


                CREATE FUNCTION "Update_LastUpdated_Function"() RETURNS TRIGGER LANGUAGE PLPGSQL AS $$
                BEGIN
                    NEW."LastUpdated" := now();
                    RETURN NEW;
                END;
                $$;

                CREATE TRIGGER "UpdateLastUpdated"
                    BEFORE INSERT OR UPDATE
                    ON "Movies"."Movies"
                    FOR EACH ROW
                    EXECUTE FUNCTION "Update_LastUpdated_Function"();

                CREATE TRIGGER "UpdateLastUpdated"
                    BEFORE INSERT OR UPDATE
                    ON "Watchlists"."Watchlists"
                    FOR EACH ROW
                    EXECUTE FUNCTION "Update_LastUpdated_Function"();

                CREATE TRIGGER "UpdateLastUpdated"
                    BEFORE INSERT OR UPDATE
                    ON "Watchlists"."WatchlistsMovies"
                    FOR EACH ROW
                    EXECUTE FUNCTION "Update_LastUpdated_Function"();
            

ALTER TABLE "Watchlists"."WatchlistsMovies" ADD "Created" timestamp with time zone NOT NULL DEFAULT (now());

ALTER TABLE "Watchlists"."WatchlistsMovies" ADD "LastUpdated" timestamp with time zone NOT NULL DEFAULT (now());

ALTER TABLE "Watchlists"."Watchlists" ADD "Created" timestamp with time zone NOT NULL DEFAULT (now());

ALTER TABLE "Watchlists"."Watchlists" ADD "LastUpdated" timestamp with time zone NOT NULL DEFAULT (now());

ALTER TABLE "Movies"."Movies" ADD "Created" timestamp with time zone NOT NULL DEFAULT (now());

ALTER TABLE "Movies"."Movies" ADD "LastUpdated" timestamp with time zone NOT NULL DEFAULT (now());

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230919171249_CreatedAndLastUpdated', '7.0.8');

COMMIT;

