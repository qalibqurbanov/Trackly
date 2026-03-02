using System.Collections.Generic;
using System.Threading.Tasks;
using Trackly.UI.Data.Models.Entitities;

namespace Trackly.UI.Data.Repositories.Reddit;

public interface IRedditRepository
{
    Task<List<RedditSubreddit>> GetAllSubredditsAsync();
    Task<List<RedditPost>> GetAllPostsAsync();
    Task<List<RedditPost>> GetPostsBySubredditAsync(string subredditName);
    Task<bool> SubredditExistsAsync(string displayName);
    void AddSubreddit(RedditSubreddit subreddit);
    Task<RedditSubreddit> GetSubredditWithPostsAsync(string subredditName);
    void RemoveSubreddit(RedditSubreddit subreddit);
    Task<bool> PostExistsAsync(string postUrl);
    void AddPosts(List<RedditPost> posts);
    void UpdateSubreddit(RedditSubreddit subreddit);
    Task SaveAsync();
}