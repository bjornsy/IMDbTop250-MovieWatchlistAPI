CREATE SCHEMA Movies
CREATE TABLE Movies.Top250 (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid (),
    Ranking INT,
    Title TEXT,
    Rating DECIMAL
);

COPY Movies.Top250(Ranking, Title, Rating) FROM '/docker-entrypoint-initdb.d/top250Movies.csv' DELIMITER ',' CSV HEADER;