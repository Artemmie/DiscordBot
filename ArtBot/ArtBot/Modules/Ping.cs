using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArtBot.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync(string name)
        {
            await ReplyAsync($" {name} is a noob");
        }
        //  public async Task TaskAsync()
        //  {
        //  EmbedBuilder builder = new EmbedBuilder();

        //  builder.WithTitle("Ping!")
        //      .WithDescription("ping")
        //      .WithColor(Color.Blue);

        //  builder.AddField("Field1", "Test")
        //      .AddInlineField("Field2", "Test")
        //     .AddInlineField("Field3", "Test");

        //  await ReplyAsync("", false, builder.Build());

        // Context.User;
        // Context.Client;
        // Context.Guild;
        // Context.Message;
        // Context.Channel;

        // await ReplyAsync($" {Context.Client.CurrentUser.Mention} || {Context.User.Mention} sent {Context.Message.Content} in {Context.Guild.Name}!");
        // }
    }
}
