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
        private ApplicationSettings _appSettings = null;

        public Discord()
        {
            _appSettings = Configuration.Configuration.GetConfiguration();
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
            await _client.LoginAsync(TokenType.Bot, _appSettings.DiscordToken);
            await _client.StartAsync();

            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                return Task.CompletedTask;
            };

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public void SendMessage(string message, string url)
        {
            while (_client.ConnectionState != global::Discord.ConnectionState.Connected)
                System.Threading.Thread.Sleep(1000);

            var channel = _client.GetChannel(Convert.ToUInt64(_appSettings.BotChannelId)) as IMessageChannel;
            channel.SendMessageAsync(message + Environment.NewLine + url);
        }
    }
}
