using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Trackly.UI.Helpers.Http;
using Trackly.UI.Helpers.Forms;
using System.Collections.Generic;
using Trackly.UI.Data.DbContexts;
using Trackly.UI.Services.Logging;
using Microsoft.EntityFrameworkCore;
using Trackly.UI.Data.Models.Entitities;
using Trackly.UI.Data.Repositories.Reddit;

namespace Trackly.UI.Forms;

public partial class MainForm : Form
{
    #region Vars
    private readonly TracklyDbContext _dbContext = new TracklyDbContext();
    private readonly IRedditRepository _redditRepository;
    #endregion Vars

    #region Constructor & Load
    public MainForm()
    {
        InitializeComponent();

        #region Styling
        #region Reddit
        #region ListView
        FormStylingUtils.ConfigureListView
        (
            control_reddit_manage_listview_subreddits,
            new string[] { "Subreddit Name", "Last Checked Post Date" },
            new int[] { 85, 15 }
        );

        FormStylingUtils.ConfigureListView
        (
            control_reddit_logs_listview_logs,
            new string[] { "Event Type", "Message", "Timestamp" },
            new int[] { 20, 65, 15 }
        );

        FormStylingUtils.ConfigureListView
        (
            control_reddit_feed_listview_feeds,
            new string[] { "Subreddit Name", "Post Title", "Published Date" },
            new int[] { 20, 75, 15 }
        );
        #endregion ListView

        #region Textbox
        FormStylingUtils.ConfigurePlaceholderForTextbox(control_reddit_manage_textbox_url, "Enter subreddit URL...");
        FormStylingUtils.ConfigurePlaceholderForTextbox(control_reddit_feed_textbox_search, "Search text...");
        #endregion Textbox
        #endregion Reddit

        #endregion Styling

        _dbContext.Database.Migrate();
        _redditRepository = new RedditRepository(_dbContext);
    }

    private async void MainForm_LoadAsync(object sender, EventArgs e)
    {
        await GetSubredditsAndPostsFromDatabaseAndFillListviewsAndComboboxAsync("Loaded {0} subreddits and {1} posts on startup");
    }
    #endregion Constructor & Load

    #region Reddit
    #region Misc
    private async Task GetSubredditsAndPostsFromDatabaseAndFillListviewsAndComboboxAsync(string logMessage)
    {
        try
        {
            var subreddits = await _redditRepository.GetAllSubredditsAsync();

            #region Fill Subreddit ListView
            control_reddit_manage_listview_subreddits.Items.Clear();
            foreach (var subreddit in subreddits)
            {
                ListViewItem item = new ListViewItem(subreddit.SubredditName ?? "(No name)");
                item.SubItems.Add(subreddit.LastPostDate?.ToString("yyyy-MM-dd HH:mm") ?? "");
                control_reddit_manage_listview_subreddits.Items.Add(item);
            }
            #endregion Fill Subreddit ListView

            #region Fill Feed Combobox
            control_reddit_feed_combobox_subredditList.Items.Clear();
            control_reddit_feed_combobox_subredditList.Items.Add("ALL");
            foreach (var subreddit in subreddits)
            {
                control_reddit_feed_combobox_subredditList.Items.Add(subreddit.SubredditName);
            }

            // Setting SelectedIndex would trigger the "item changed" event and cause a redundant
            // database reload of the feed listview, which we are already about to fill manually below.
            // Disconnecting it first prevents that unnecessary double database call.
            control_reddit_feed_combobox_subredditList.SelectedIndexChanged -= Control_reddit_feed_combobox_subredditList_SelectedIndexChanged;
            control_reddit_feed_combobox_subredditList.SelectedIndex = 0;
            // Reconnect now that the combobox value is set and the listview is about to be filled correctly.
            control_reddit_feed_combobox_subredditList.SelectedIndexChanged += Control_reddit_feed_combobox_subredditList_SelectedIndexChanged;
            #endregion Fill Feed Combobox

            #region Fill Feed ListView
            var posts = await _redditRepository.GetAllPostsAsync();

            control_reddit_feed_listview_feeds.Items.Clear();
            foreach (var post in posts)
            {
                string subredditName = post.Subreddit?.SubredditName ?? "(Unknown)";
                ListViewItem item = new ListViewItem(subredditName);
                item.SubItems.Add(post.PostName);
                item.SubItems.Add(post.PublishedDate.ToString("yyyy-MM-dd HH:mm"));
                control_reddit_feed_listview_feeds.Items.Add(item);
            }
            #endregion Fill Feed ListView

            LogService.AddLog(control_reddit_logs_listview_logs, "Load", string.Format(logMessage, subreddits.Count, posts.Count));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading initial subreddit data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogService.AddLog(control_reddit_logs_listview_logs, "Error", $"Error loading initial subreddit data: {ex.Message}");
        }
    }
    #endregion Misc

    #region Manage
    #region Add & Check
    private async void control_reddit_manage_button_add_Click(object sender, EventArgs e)
    {
        string url = control_reddit_manage_textbox_url.Text.Trim();
        if (string.IsNullOrEmpty(url))
        {
            MessageBox.Show("Please enter a subreddit URL.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string subredditName = null;
        try
        {
            Uri uri = new Uri(url);
            string[] parts = uri.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2 && parts[0].Equals("r", StringComparison.OrdinalIgnoreCase))
                subredditName = parts[1];
        }
        catch
        {
            MessageBox.Show("Invalid subreddit URL.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (string.IsNullOrEmpty(subredditName))
        {
            MessageBox.Show("Cannot extract subreddit name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Verify the subreddit actually exists on Reddit before saving it
        bool existsOnReddit = await HttpUtils.SubredditExistsOnRedditAsync(subredditName);
        if (!existsOnReddit)
        {
            MessageBox.Show($"The subreddit \"r/{subredditName}\" does not exist on Reddit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        string displayName = $"r/{subredditName}";
        if (await _redditRepository.SubredditExistsAsync(displayName))
        {
            MessageBox.Show("This subreddit is already added.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var newSubreddit = new RedditSubreddit
        {
            SubredditName = displayName,
            SubredditUrl = url,
            LastPostDate = null
        };
        _redditRepository.AddSubreddit(newSubreddit);
        await _redditRepository.SaveAsync();

        ListViewItem newItem = new ListViewItem(displayName);
        newItem.SubItems.Add("");
        control_reddit_manage_listview_subreddits.Items.Add(newItem);

        LogService.AddLog(control_reddit_logs_listview_logs, "Add Subreddit", $"Added subreddit \"{displayName}\"");

        control_reddit_manage_textbox_url.Clear();
    }

    private async void control_reddit_manage_button_check_Click(object sender, EventArgs e)
    {
        string defaultTitle = this.Text;
        try
        {
            this.Text = $"{defaultTitle} (Loading...: Checking subreddits)";

            // When we set the combobox value in code, it accidentally triggers the combobox's "item changed" function.
            // That function goes to the database and reloads the feed listview from scratch.
            // The problem is that this database reload happens in the background (async), so its timing is unpredictable.
            // It can finish AFTER our check loop has already added the new posts to the listview, and when it does,
            // it wipes the listview clean and redraws it with only the old data — because the new posts haven't been
            // saved to the database yet at that moment. This makes it look like the check did nothing.
            // To prevent this, we temporarily disconnect that "item changed" function before we touch the combobox,
            // then reconnect it right after, so it doesn't fire at all during this part.
            control_reddit_feed_combobox_subredditList.SelectedIndexChanged -= Control_reddit_feed_combobox_subredditList_SelectedIndexChanged;
            control_reddit_feed_combobox_subredditList.SelectedItem = "ALL";
            // The "item changed" function is reconnected here and will work normally from this point on.
            control_reddit_feed_combobox_subredditList.SelectedIndexChanged += Control_reddit_feed_combobox_subredditList_SelectedIndexChanged;

            var subreddits = await _redditRepository.GetAllSubredditsAsync();
            int totalNewPosts = 0;

            #region Progress Bar
            int totalPostsEstimate = subreddits.Count * 100;
            control_reddit_manage_progressbar_checkStatus.Minimum = 0;
            control_reddit_manage_progressbar_checkStatus.Maximum = totalPostsEstimate;
            control_reddit_manage_progressbar_checkStatus.Value = 0;
            #endregion Progress Bar

            foreach (var subreddit in subreddits)
            {
                #region Fetch New Posts
                string after = null;
                bool hasMore = true;
                DateTime? latestPostDate = subreddit.LastPostDate;
                var newPosts = new List<RedditPost>();

                while (hasMore)
                {
                    string subredditName = subreddit.SubredditName?.Replace("r/", "") ?? "";
                    if (string.IsNullOrWhiteSpace(subredditName))
                        break;

                    string url = $"https://www.reddit.com/r/{subredditName}/new.json?limit=100";
                    if (!string.IsNullOrEmpty(after))
                        url += $"&after={after}";

                    string response = await HttpUtils.GetStringAsync(url);
                    var json = System.Text.Json.JsonDocument.Parse(response);
                    var postsArray = json.RootElement.GetProperty("data").GetProperty("children");

                    foreach (var post in postsArray.EnumerateArray())
                    {
                        try
                        {
                            var postData = post.GetProperty("data");
                            string postTitle = postData.TryGetProperty("title", out var t) ? t.GetString() ?? "(No title)" : "(No title)";
                            string postUrl = postData.TryGetProperty("url", out var u) ? u.GetString() ?? "(No URL)" : "(No URL)";

                            double unixTimeDouble = 0;
                            if (postData.TryGetProperty("created_utc", out var c))
                            {
                                if (c.TryGetInt64(out var unixTime)) unixTimeDouble = unixTime;
                                else if (c.TryGetDouble(out var unixTimeD)) unixTimeDouble = unixTimeD;
                            }
                            DateTime publishedDate = DateTimeOffset.FromUnixTimeSeconds((long)unixTimeDouble).UtcDateTime;

                            if (!await _redditRepository.PostExistsAsync(postUrl))
                            {
                                var newPost = new RedditPost
                                {
                                    PostName = postTitle,
                                    PostUrl = postUrl,
                                    PublishedDate = publishedDate,
                                    SubredditId = subreddit.Id
                                };
                                newPosts.Add(newPost);

                                if (!latestPostDate.HasValue || publishedDate > latestPostDate.Value)
                                    latestPostDate = publishedDate;
                            }

                            if (control_reddit_manage_progressbar_checkStatus.Value < control_reddit_manage_progressbar_checkStatus.Maximum)
                                control_reddit_manage_progressbar_checkStatus.Value++;
                        }
                        catch { continue; }
                    }

                    after = json.RootElement.GetProperty("data").GetProperty("after").GetString();
                    if (string.IsNullOrEmpty(after)) hasMore = false;
                }

                if (newPosts.Any())
                {
                    _redditRepository.AddPosts(newPosts);
                    totalNewPosts += newPosts.Count;
                    LogService.AddLog(control_reddit_logs_listview_logs, "New Posts", $"Added {newPosts.Count} new posts from {subreddit.SubredditName}");
                }

                if (latestPostDate.HasValue)
                {
                    subreddit.LastPostDate = latestPostDate.Value;
                    _redditRepository.UpdateSubreddit(subreddit);

                    foreach (ListViewItem item in control_reddit_manage_listview_subreddits.Items)
                        if (item.Text == subreddit.SubredditName)
                            item.SubItems[1].Text = latestPostDate.Value.ToString("yyyy-MM-dd HH:mm");
                }

                // Single save per subreddit — covers both the new posts and the updated LastPostDate together
                await _redditRepository.SaveAsync();

                #endregion Fetch New Posts
            }

            control_reddit_manage_progressbar_checkStatus.Value = control_reddit_manage_progressbar_checkStatus.Maximum;

            // Do one clean reload from DB after everything is saved — no race conditions
            await GetSubredditsAndPostsFromDatabaseAndFillListviewsAndComboboxAsync("Reloaded {0} subreddits and {1} posts after check");

            MessageBox.Show("Subreddits checked successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LogService.AddLog(control_reddit_logs_listview_logs, "Check Subreddits", $"Completed subreddit checking — {subreddits.Count} subreddits checked, {totalNewPosts} new posts found");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error checking subreddits: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogService.AddLog(control_reddit_logs_listview_logs, "Error", $"Error checking subreddits: {ex.Message}");
        }
        finally
        {
            this.Text = defaultTitle;
        }
    }
    #endregion Add & Check

    #region Context Menu
    private async void control_reddit_manage_menuFor_listview_subreddits_Remove_Click(object sender, EventArgs e)
    {
        if (control_reddit_manage_listview_subreddits.SelectedItems.Count == 0)
        {
            MessageBox.Show("Please select a subreddit to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var selectedItem = control_reddit_manage_listview_subreddits.SelectedItems[0];
        string subredditName = selectedItem.Text;

        var confirm = MessageBox.Show(
            $"Are you sure you want to remove the subreddit \"{subredditName}\" and all its posts?",
            "Confirm Removal",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
            return;

        try
        {
            var subreddit = await _redditRepository.GetSubredditWithPostsAsync(subredditName);

            if (subreddit != null)
            {
                _redditRepository.RemoveSubreddit(subreddit);
                await _redditRepository.SaveAsync();

                control_reddit_manage_listview_subreddits.Items.Remove(selectedItem);

                for (int i = control_reddit_feed_listview_feeds.Items.Count - 1; i >= 0; i--)
                {
                    var item = control_reddit_feed_listview_feeds.Items[i];
                    if (item.Text == subredditName)
                        control_reddit_feed_listview_feeds.Items.RemoveAt(i);
                }

                LogService.AddLog(control_reddit_logs_listview_logs, "Remove Subreddit",
                    $"Removed subreddit \"{subredditName}\" and its posts");
            }
            else
            {
                MessageBox.Show("Subreddit not found in database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error removing subreddit: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            LogService.AddLog(control_reddit_logs_listview_logs, "Error", $"Error removing subreddit \"{subredditName}\": {ex.Message}");
        }
    }
    #endregion Context Menu
    #endregion Manage

    #region Logs
    #endregion Logs

    #region Feed
    #region Subreddit Filter & Search
    private async void Control_reddit_feed_combobox_subredditList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selected = control_reddit_feed_combobox_subredditList.SelectedItem?.ToString();
        control_reddit_feed_listview_feeds.Items.Clear();

        List<RedditPost> posts = (selected == "ALL")
            ? await _redditRepository.GetAllPostsAsync()
            : await _redditRepository.GetPostsBySubredditAsync(selected);

        foreach (var post in posts)
        {
            string subredditName = post.Subreddit?.SubredditName ?? "(Unknown)";
            ListViewItem item = new ListViewItem(subredditName);
            item.SubItems.Add(post.PostName);
            item.SubItems.Add(post.PublishedDate.ToString("yyyy-MM-dd HH:mm"));
            control_reddit_feed_listview_feeds.Items.Add(item);
        }

        LogService.AddLog(control_reddit_logs_listview_logs, "Filter Posts", $"Filtered posts by \"{selected}\"");
    }

    private async void control_reddit_feed_textbox_search_TextChanged(object sender, EventArgs e)
    {
        string searchText = control_reddit_feed_textbox_search.Text.Trim().ToLower();
        string selectedSubreddit = control_reddit_feed_combobox_subredditList.SelectedItem?.ToString();
        control_reddit_feed_listview_feeds.Items.Clear();

        List<RedditPost> posts = (selectedSubreddit == "ALL")
            ? await _redditRepository.GetAllPostsAsync()
            : await _redditRepository.GetPostsBySubredditAsync(selectedSubreddit);

        if (!string.IsNullOrEmpty(searchText))
            posts = posts.Where(p => p.PostName?.ToLower().Contains(searchText) == true).ToList();

        foreach (var post in posts)
        {
            string subredditName = post.Subreddit?.SubredditName ?? "(Unknown)";
            ListViewItem item = new ListViewItem(subredditName);
            item.SubItems.Add(post.PostName);
            item.SubItems.Add(post.PublishedDate.ToString("yyyy-MM-dd HH:mm"));
            control_reddit_feed_listview_feeds.Items.Add(item);
        }

        LogService.AddLog(control_reddit_logs_listview_logs, "Search Posts",
            $"Filtered posts by \"{searchText}\" in \"{selectedSubreddit}\"");
    }
    #endregion Subreddit Filter & Search
    #endregion Feed
    #endregion Reddit
}