using Discord;
using Discord.WebSocket;
using RSSNotify.Configuration;
using System;
using System.Threading.Tasks;

namespace RSSNotify.Processes
{
    public class Discord
    {
        private DiscordSocketClient _client;
        private PollerInstance _pollerInstance = null;

        public Discord(PollerInstance pollerInstance)
        {
            _pollerInstance = pollerInstance;
            Initialize();
        }

        public ConnectionState GetConnectionState()
        {
            return _client.ConnectionState;
        }

        public async Task Initialize()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, _pollerInstance.DiscordToken);
            await _client.StartAsync();

            _client.Ready += () =>
            {
                Console.WriteLine($"({_pollerInstance.Description}) " + "Bot is connected!");
                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine($"({_pollerInstance.Description}) " + msg.ToString());
            return Task.CompletedTask;
        }

        public void SendMessage(string message, string url)
        {
            while (_client.ConnectionState != global::Discord.ConnectionState.Connected)
                System.Threading.Thread.Sleep(1000);

            var channel = _client.GetChannel(Convert.ToUInt64(_pollerInstance.DiscordChannelId)) as IMessageChannel;
            channel.SendMessageAsync(message + Environment.NewLine + url);
        }
    }
}
