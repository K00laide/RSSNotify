using RSSNotify.Configuration;
using System;
using System.Linq;

namespace RSSNotify.Processes
{
    public class RSSPoller
    {
        private Discord _discordWorker = null;
        private DateTimeOffset _lastDateTime = DateTime.UtcNow;
        private ApplicationSettings _appSettings = null;

        public RSSPoller()
        {
            _appSettings = Configuration.Configuration.GetConfiguration();
            _discordWorker = new Discord();
        }

        public void PollFeed()
        {
            var reader = new RSSReader(_appSettings.RSSFeedUrl);
            var items = reader.GetFeedItems();
            var newItems = items.Where(x => x.LastUpdatedTime > _lastDateTime);

            if (!newItems.Any())
                return;

            foreach(var newItem in newItems)
                _discordWorker.SendMessage(newItem.Title.Text, newItem.Links[0].Uri.ToString());

            _lastDateTime = newItems.Max(x => x.LastUpdatedTime);
        }
    }
}
