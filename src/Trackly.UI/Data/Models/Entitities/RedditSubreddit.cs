using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trackly.UI.Data.Models.Entitities;

public class RedditSubreddit
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string SubredditName { get; set; } // r/subredditName

    [Required]
    public string SubredditUrl { get; set; }

    public DateTime? LastPostDate { get; set; }

    public ICollection<RedditPost> Posts { get; set; }
}