﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TwitterClone.Data;

namespace TwitterClone.Migrations
{
    [DbContext(typeof(TwitterCloneContext))]
    partial class TwitterCloneContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("TwitterClone.Data.Models.Tweet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnName("body")
                        .HasMaxLength(256);

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp");

                    b.Property<int?>("author")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("author");

                    b.ToTable("tweets");
                });

            modelBuilder.Entity("TwitterClone.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnName("password_hash");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("TwitterClone.Data.Models.Tweet", b =>
                {
                    b.HasOne("TwitterClone.Data.Models.User", "Author")
                        .WithMany("Tweets")
                        .HasForeignKey("author")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}