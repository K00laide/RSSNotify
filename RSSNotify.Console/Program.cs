using RSSNotify.Configuration;
using RSSNotify.Processes;

namespace RSSNotify.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Fetching configuration");
            var appSettings = Configuration.Configuration.GetConfiguration();

            System.Console.WriteLine("Initializing Poller");
            var poller = new RSSPoller();

            var pollerDelay = appSettings.PollerDelay;
            System.Console.WriteLine($"Begining polling with delay of {pollerDelay}");

            while (true == true)
            {
                System.Console.WriteLine("Polling feed for new items");
                poller.PollFeed();
                System.Threading.Thread.Sleep(pollerDelay);
            }
        }
    }
}
