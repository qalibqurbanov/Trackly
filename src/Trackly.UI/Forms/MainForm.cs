using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trackly.UI.Data.DbContexts;
using Trackly.UI.Data.Models.Entitities;
using Trackly.UI.Helpers.Forms;
using Trackly.UI.Services.Logging;

namespace Trackly.UI.Forms;

public partial class MainForm : Form
{
    #region Vars
    private readonly TracklyDbContext _dbContext = new TracklyDbContext();
    #endregion Vars

    public MainForm()
    {
        InitializeComponent();

        #region Styling
        #region Reddit ListViews
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
        #endregion Reddit ListViews
        #endregion Styling

        _dbContext.Database.Migrate();
    }

    private async void MainForm_LoadAsync(object sender, EventArgs e)
    {
    }
}