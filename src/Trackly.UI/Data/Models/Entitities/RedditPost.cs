using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trackly.UI.Data.Models.Entitities;

public class RedditPost
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string PostName { get; set; }

    [Required]
    public string PostUrl { get; set; }

    [Required]
    public DateTime PublishedDate { get; set; }

    [Required]
    public int SubredditId { get; set; }
    [ForeignKey(nameof(SubredditId))]
    public RedditSubreddit Subreddit { get; set; }
}