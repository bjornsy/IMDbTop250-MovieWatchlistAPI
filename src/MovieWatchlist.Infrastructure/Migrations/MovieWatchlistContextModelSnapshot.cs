﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieWatchlist.Infrastructure.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieWatchlist.Infrastructure.Migrations
{
    [DbContext(typeof(MovieWatchlistContext))]
    partial class MovieWatchlistContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MovieWatchlist.ApplicationCore.Models.Movie", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int?>("Ranking")
                        .HasColumnType("integer");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric(2,1)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Movies", "Movies");
                });

            modelBuilder.Entity("MovieWatchlist.ApplicationCore.Models.Watchlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Watchlists", "Watchlists");
                });

            modelBuilder.Entity("MovieWatchlist.ApplicationCore.Models.WatchlistsMovies", b =>
                {
                    b.Property<string>("MovieId")
                        .HasColumnType("text");

                    b.Property<Guid>("WatchlistId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Watched")
                        .HasColumnType("boolean");

                    b.HasKey("MovieId", "WatchlistId");

                    b.HasIndex("WatchlistId");

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("WatchlistId"), new[] { "MovieId" });

                    b.ToTable("WatchlistsMovies", "Watchlists");
                });

            modelBuilder.Entity("MovieWatchlist.ApplicationCore.Models.WatchlistsMovies", b =>
                {
                    b.HasOne("MovieWatchlist.ApplicationCore.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieWatchlist.ApplicationCore.Models.Watchlist", null)
                        .WithMany()
                        .HasForeignKey("WatchlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
