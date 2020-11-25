using RSSNotify.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSSNotify.Processes
{
    public class MultithreadedPoller
    {
        public static ApplicationSettings _appSettings
        {
            get
            {
                return Configuration.Configuration.GetConfiguration();
            }
        }

        public void LaunchMultiThreadedPollers(CancellationToken stoppingToken)
        {
            var pollerInstances = _appSettings.PollerInstances;
            Console.WriteLine($"Launching {pollerInstances.Count} poller instances...");
            var tasks = new List<Task>();
            foreach(var pollerInstance in pollerInstances)
            {
                tasks.Add(Task.Factory.StartNew(() => new RSSPoller(pollerInstance).DoIt(stoppingToken)));
                Thread.Sleep(_appSettings.StartupDelay);
            }                
            Task.WaitAll(tasks.ToArray());
        }
    }
}
