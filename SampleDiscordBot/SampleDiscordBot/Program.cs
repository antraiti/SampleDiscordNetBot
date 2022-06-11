using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Net;

namespace SampleDiscordBot
{
	public class Program
	{
		private DiscordSocketClient _client;
		private CommandHandler _commandHandler;
		private CommandService _commandService;

		private readonly string _token = "YOUR_TOKEN_HERE"; 

		static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult(); 

		public async Task MainAsync()
		{
			Console.WriteLine("starting"); 
			// When working with events that have Cacheable<IMessage, ulong> parameters,
			// you must enable the message cache in your config settings if you plan to
			// use the cached message entity. 
			var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
			_client = new DiscordSocketClient(_config);

			Console.WriteLine("Getting client");
			await _client.LoginAsync(TokenType.Bot, _token);
			await _client.StartAsync();

			_commandService = new CommandService();
			_commandHandler = new CommandHandler(_client, _commandService);
			_commandHandler.InstallCommandsAsync();

			_client.MessageUpdated += MessageUpdated;
			_client.Ready += () =>
			{
				Console.WriteLine("Bot is connected!");
				return Task.CompletedTask;
			};

			await Task.Delay(-1);
		}

		private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
		{
			// If the message was not in the cache, downloading it will result in getting a copy of `after`.
			var message = await before.GetOrDownloadAsync();
			Console.WriteLine($"{message} -> {after}");
		}
	}
}