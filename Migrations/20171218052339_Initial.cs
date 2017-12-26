using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TwitterClone.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    email_address = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: false),
                    last_name = table.Column<string>(nullable: false),
                    password_hash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tweets",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    body = table.Column<string>(maxLength: 256, nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false),
                    author = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweets", x => x.id);
                    table.ForeignKey(
                        name: "FK_tweets_users_author",
                        column: x => x.author,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tweets_author",
                table: "tweets",
                column: "author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tweets");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
