using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Trackly.UI.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Trackly.UI.Data.Models.Entitities;

namespace Trackly.UI.Data.Repositories.Reddit;

public class RedditRepository : IRedditRepository
{
    private readonly TracklyDbContext _dbContext;
    public RedditRepository(TracklyDbContext dbContext) => this._dbContext = dbContext;

    public async Task<List<RedditSubreddit>> GetAllSubredditsAsync()
    {
        return await _dbContext.RedditSubreddits.ToListAsync();
    }

    public async Task<List<RedditPost>> GetAllPostsAsync()
    {
        return await _dbContext.RedditPosts
            .Include(p => p.Subreddit)
            .OrderByDescending(p => p.PublishedDate)
            .ToListAsync();
    }

    public async Task<List<RedditPost>> GetPostsBySubredditAsync(string subredditName)
    {
        return await _dbContext.RedditPosts
            .Include(p => p.Subreddit)
            .Where(p => p.Subreddit.SubredditName == subredditName)
            .OrderByDescending(p => p.PublishedDate)
            .ToListAsync();
    }

    public async Task<bool> SubredditExistsAsync(string displayName)
    {
        return await _dbContext.RedditSubreddits.AnyAsync(s => s.SubredditName == displayName);
    }

    public void AddSubreddit(RedditSubreddit subreddit)
    {
        _dbContext.RedditSubreddits.Add(subreddit);
    }

    public async Task<RedditSubreddit> GetSubredditWithPostsAsync(string subredditName)
    {
        return await _dbContext.RedditSubreddits
            .Include(s => s.Posts)
            .FirstOrDefaultAsync(s => s.SubredditName == subredditName);
    }

    public void RemoveSubreddit(RedditSubreddit subreddit)
    {
        if (subreddit.Posts != null && subreddit.Posts.Any())
        {
            _dbContext.RedditPosts.RemoveRange(subreddit.Posts);
        }

        _dbContext.RedditSubreddits.Remove(subreddit);
    }

    public async Task<bool> PostExistsAsync(string postUrl)
    {
        return await _dbContext.RedditPosts.AnyAsync(p => p.PostUrl == postUrl);
    }

    public void AddPosts(List<RedditPost> posts)
    {
        _dbContext.RedditPosts.AddRange(posts);
    }

    public void UpdateSubreddit(RedditSubreddit subreddit)
    {
        _dbContext.RedditSubreddits.Update(subreddit);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}