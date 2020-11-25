using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RSSNotify.Configuration
{
    [JsonObject("applicationSettings")]
    public class ApplicationSettings
    {
        [JsonProperty("pollerInstances")]
        public List<PollerInstance> PollerInstances { get; set; }
        [JsonProperty("startUpDelay")]
        public int StartupDelay { get; set; }
    }

    public class PollerInstance
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("delay")]
        public int Delay { get; set; }

        [JsonProperty("rssFeedUrl")]
        public string RSSFeedUrl { get; set; }

        [JsonProperty("discordToken")]
        public string DiscordToken { get; set; }

        [JsonProperty("discordChannelId")]
        public string DiscordChannelId { get; set; }

        [JsonProperty("titleFilters")]
        public List<string> TitleFilters { get; set; }
    }
}
