using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace RSSNotify.Configuration
{
    [JsonObject("applicationSettings")]
    public class ApplicationSettings
    {
        [JsonProperty("pollerDelay")]
        public int PollerDelay { get; set; }

        [JsonProperty("rssFeedUrl")]
        public string RSSFeedUrl { get; set; }

        [JsonProperty("discordToken")]
        public string DiscordToken { get; set; }

        [JsonProperty("botChannelId")]
        public string BotChannelId { get; set; }
    }
}
