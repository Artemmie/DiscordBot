﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace ArtBot
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string botToken = "NDM4NzI5MzQ3NjUwNTUxODA5.DcI_ww.BZBTXXF8ms7pmIch6isE8-vI8p4";

            //event subscriptions
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;

            await RegisterCommandAsync();

            await _client.LoginAsync(TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync($"Welcome, {user.Mention}");
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var conext = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(conext, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
