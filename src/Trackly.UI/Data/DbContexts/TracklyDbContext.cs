using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Trackly.UI.Data.Models.Entitities;

namespace Trackly.UI.Data.DbContexts;

public class TracklyDbContext : DbContext
{
    public TracklyDbContext() : base
    (
        new DbContextOptionsBuilder<TracklyDbContext>()
            .UseSqlite($"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage.db")}")
            .Options
    ) { }

    public DbSet<RedditSubreddit> RedditSubreddits { get; set; }
    public DbSet<RedditPost> RedditPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}