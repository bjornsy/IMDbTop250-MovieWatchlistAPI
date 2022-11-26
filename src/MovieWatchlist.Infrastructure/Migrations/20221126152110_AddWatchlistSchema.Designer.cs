﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieWatchlist.Infrastructure.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieWatchlist.Infrastructure.Migrations
{
    [DbContext(typeof(MovieWatchlistContext))]
    [Migration("20221126152110_AddWatchlistSchema")]
    partial class AddWatchlistSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
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
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Watchlists", "Watchlists");
                });

            modelBuilder.Entity("MovieWatchlist.ApplicationCore.Models.WatchlistsMovies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MovieId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WatchlistId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WatchlistsMovies", "Watchlists");
                });
#pragma warning restore 612, 618
        }
    }
}
