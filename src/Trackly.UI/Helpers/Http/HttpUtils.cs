using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trackly.UI.Helpers.Http;

public class HttpUtils
{
    private static readonly HttpClient _httpClient;

    static HttpUtils()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Trackly");
    }

    public static async Task<string> GetStringAsync(string url)
    {
        return await _httpClient.GetStringAsync(url);
    }

    public static async Task<bool> SubredditExistsOnRedditAsync(string subredditName)
    {
        try
        {
            string response = await _httpClient.GetStringAsync($"https://www.reddit.com/r/{subredditName}/about.json");
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