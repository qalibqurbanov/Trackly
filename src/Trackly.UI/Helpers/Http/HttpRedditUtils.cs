using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trackly.UI.Helpers.Http;

public class HttpRedditUtils
{
    public static async Task<bool> SubredditExistsOnRedditAsync(string subredditName)
    {
        try
        {
            string response = await HttpUtils.GetStringAsync($"https://www.reddit.com/r/{subredditName}/about.json");
            var json = System.Text.Json.JsonDocument.Parse(response);
            string kind = json.RootElement.GetProperty("kind").GetString();

            return kind == "t5";
        }
        catch
        {
            return false;
        }
    }
}