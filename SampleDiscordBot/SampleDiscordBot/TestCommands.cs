﻿using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDiscordBot
{
	//These should be dummy commands with no real access to anything for testing/learning
	//All commands need to be public and extend ModuleBase
	public class TestCommands
	{
		public class TestModule : ModuleBase<SocketCommandContext>
		{
			[Command("echo")]
			[Summary("returns what you say")]
			public async Task SimpleEcho([Summary("what you say")] string receivedText)
			{
				await ReplyAsync(receivedText);
			}

			[Command("username")]
			[Summary("returns your username")]
			public async Task SimpleUsername()
			{
				await ReplyAsync(Context.User.Username);
			}
		}

		[Group("testing")]
		public class TestGroupModule : ModuleBase<SocketCommandContext>
		{
			//this command since in a group will be accessed by doing "-testing echo"
			[Command("echo")]
			[Summary("returns what you say")]
			public async Task SimpleTestingEcho([Summary("what you say")] string receivedText)
			{
				await ReplyAsync(receivedText);
			}

			[Command("channelname")]
			[Summary("returns the channel name")]
			public async Task SimpleTestingChannelName()
			{
				await ReplyAsync(Context.Channel.Name);
			}
		}
	}
}
