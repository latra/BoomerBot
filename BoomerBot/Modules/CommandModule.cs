using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
namespace BoomerBot.Modules
{
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        [Command("Ping")]
        public async Task Ping()
        {
            var ping = DateTime.Now - this.Context.Message.CreatedAt;
            await ReplyAsync(String.Format("Pong: {0}ms", Math.Abs(ping.TotalMilliseconds)));
        }
        [Command("Help")]
        public async Task Help()
        {
            await ReplyAsync("Deja que te cuente una historia...\n" +
                "\t- Si mencionas la palabra boomer, me ofenderé.\n" +
                "\t- Si lloras, me ofenderé.\n" +
                "\t- Si me llamas rojo, me ofenderé.\n" +
                "\t- Si insultas, me ofenderé.\n" +
                "\t- Si usas el comando okb!ping, no me ofenderé y te diré la latencia.\n" +
                "\t- Si usas el comando okb!ping, ya sabes lo que pasa.");

        }
    }
}
