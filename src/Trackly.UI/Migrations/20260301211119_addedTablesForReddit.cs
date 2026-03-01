using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trackly.UI.Migrations
{
    /// <inheritdoc />
    public partial class addedTablesForReddit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RedditSubreddits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SubredditName = table.Column<string>(type: "TEXT", nullable: false),
                    SubredditUrl = table.Column<string>(type: "TEXT", nullable: false),
                    LastPostDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedditSubreddits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RedditPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostName = table.Column<string>(type: "TEXT", nullable: false),
                    PostUrl = table.Column<string>(type: "TEXT", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SubredditId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedditPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RedditPosts_RedditSubreddits_SubredditId",
                        column: x => x.SubredditId,
                        principalTable: "RedditSubreddits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedditPosts_SubredditId",
                table: "RedditPosts",
                column: "SubredditId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RedditPosts");

            migrationBuilder.DropTable(
                name: "RedditSubreddits");
        }
    }
}
