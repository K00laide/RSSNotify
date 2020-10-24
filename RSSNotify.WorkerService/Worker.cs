using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RSSNotify.Processes;

namespace RSSNotify.WorkerService
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            System.Console.WriteLine("Fetching configuration");
            var appSettings = Configuration.Configuration.GetConfiguration();

            System.Console.WriteLine("Initializing Poller");
            var poller = new RSSPoller();

            var pollerDelay = appSettings.PollerDelay;
            System.Console.WriteLine($"Begining polling with delay of {pollerDelay}");

            while (!stoppingToken.IsCancellationRequested)
            {
                System.Console.WriteLine("Polling feed for new items");
                poller.PollFeed();
                await Task.Delay(appSettings.PollerDelay, stoppingToken);
            }

            System.Console.WriteLine("Canellation has been requested");
        }
    }
}
