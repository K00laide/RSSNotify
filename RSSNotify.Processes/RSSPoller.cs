using RSSNotify.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSSNotify.Processes
{
    public class RSSPoller
    {
        private Discord _discordWorker = null;
        private DateTimeOffset _lastDateTime = DateTime.UtcNow;
        private PollerInstance _pollerInstance = null;

        public RSSPoller(PollerInstance pollerInstance)
        {
            _pollerInstance = pollerInstance;
            _discordWorker = new Discord(pollerInstance);
        }

        public void DoIt(CancellationToken stoppingToken)
        {
            var pollerDelay = _pollerInstance.Delay;
            System.Console.WriteLine($"({_pollerInstance.Description}) Begining polling with delay of {pollerDelay}");

            while (!stoppingToken.IsCancellationRequested)
            {
                System.Console.WriteLine($"({_pollerInstance.Description}) Polling feed for new items");
                PollFeed();
                Task.Delay(pollerDelay, stoppingToken).Wait();
            }

            System.Console.WriteLine($"({_pollerInstance.Description}) Canellation has been requested");
        }

        public void PollFeed()
        {
            var reader = new RSSReader(_pollerInstance.RSSFeedUrl);
            var items = reader.GetFeedItems();
            var newItems = items.Where(x => x.LastUpdatedTime > _lastDateTime);

            var titleFilters = _pollerInstance.TitleFilters;
            if (titleFilters != null && titleFilters.Any())
                newItems = newItems.Where(x => titleFilters.Any(z => x.Title.Text.ToLower().IndexOf(z.ToLower()) > -1));
            
            if (!newItems.Any())
                return;

            foreach(var newItem in newItems)
                _discordWorker.SendMessage(newItem.Title.Text, newItem.Links[0].Uri.ToString());

            _lastDateTime = newItems.Max(x => x.LastUpdatedTime);
        }
    }
}
