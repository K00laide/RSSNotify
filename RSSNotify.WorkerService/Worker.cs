using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RSSNotify.Processes;

namespace RSSNotify.WorkerService
{
    public class Worker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => new MultithreadedPoller().LaunchMultiThreadedPollers(stoppingToken));
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Cancellation has been requested, setting cancellationToken");
            await base.StopAsync(cancellationToken);
        }
    }
}
