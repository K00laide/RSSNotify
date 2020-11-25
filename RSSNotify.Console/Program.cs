using RSSNotify.Configuration;
using RSSNotify.Processes;
using System.Threading;
using System.Threading.Tasks;

namespace RSSNotify.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var cancellationToken = new CancellationToken();
            var task = Task.Run(() => new MultithreadedPoller().LaunchMultiThreadedPollers(cancellationToken));
            task.Wait();
        }
    }
}
