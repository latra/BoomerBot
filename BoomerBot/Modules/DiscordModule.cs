using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Text;
using Discord;
using BoomerBot.Models;
using Newtonsoft.Json;
using System.Configuration;
using BoomerBot.Modulos;
using System.Text.RegularExpressions;

namespace BoomerBot.Controllers
{
    public class DiscordModule
    {
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

            _client.Log += _client_Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, AppConfigModule.Config["DiscordToken"]);

            await _client.StartAsync();

            await Task.Delay(-1);

        }
        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            //LogModule.Write(message.Author.Username);
            if (message.Author.IsBot && message.Author.Id.ToString() == "709841728177307670")
                return;
            int argPos = 0;
            if (message.HasStringPrefix(AppConfigModule.Config["DiscordCommandPrefix"], ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
            else if (message.Content.ToLower() == "ok charo")
            {
                var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 4
                await chnl.SendMessageAsync("No serás tú un machirulo de esos, ¿No ?"); // 5
                
            }
            else if (message.Content.ToLower() == "ok")
            {
                var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 4
                await chnl.SendMessageAsync("boomer"); // 5
            }
            else if (isLloron(message) && isLloriqueo(message.Content.ToLower()))
            {
                var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 4
                await chnl.SendMessageAsync(getNoLlores()); // 5

            }
            else if (message.Content.ToLower().Contains("lo siento"))
            {
                var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 4
                await chnl.SendMessageAsync("Ahora no te rajes mierda seca."); // 5
            }
            else if (message.Content.ToLower().Contains("boomer"))
            {
                var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 4
                await chnl.SendMessageAsync(getInsulto()); // 5

            }
            else if (message.Content.ToLower().Contains("rojo"))
            {
                var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 

                await chnl.SendMessageAsync("¿Entiendes que quizás al que llamas rojo podria llevar una esvastika?"); // 5
            }
            else if(message.Author.IsBot && message.Content.Contains("This is the wrong pokémon"))
            {
                var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 

                await chnl.SendMessageAsync("Es que ni para cazar pokemons vales..."); // 5

            }
            else
            {
                string adjetivo = getNegativeAdj(message.Content.ToLower());
                if (!String.IsNullOrEmpty(adjetivo))
                {
                    var chnl = _client.GetChannel(message.Channel.Id) as IMessageChannel; // 
                    await chnl.SendMessageAsync(String.Format("{0} lo serás tú, {1}.", adjetivo, adjetivo.ToLower())); // 5
                }
            }
        }
        private string getNegativeAdj(string message)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("Jsons/adjetivos.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();
            var adjetivos = JsonConvert.DeserializeObject<Adjetivos>(json).adjetivos;
            foreach (string adjetivo in adjetivos)
            {
                if (message.Contains(adjetivo.ToLower()))
                    return adjetivo;
            }
            return null;
        }
        private bool isLloron(SocketUserMessage message)
        {
            foreach (SocketRole role in ((SocketGuildUser)message.Author).Roles)
            {
                if (role.Name.ToLower() == "llorón")
                    return true;
            }
            return false;
        }
        private bool isLloriqueo(string message)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("Jsons/lloros.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();
            var lloros = JsonConvert.DeserializeObject<Lloros>(json).lloros;
            foreach (string lloro in lloros)
            {
                if (message.Contains(lloro))
                    return true;
            }
            Regex buah = new Regex(@"bua*h", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex tio = new Regex(@"tii+o+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return buah.Match(message).Success || tio.Match(message).Success;
        }
        private string getInsulto()
        {

            var json = string.Empty;

            using (var fs = File.OpenRead("Jsons/insultos.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();
            var insultos = JsonConvert.DeserializeObject<Insultos>(json);
            Random random = new Random();
            return insultos.insultos[random.Next() % insultos.insultos.Length];
        }

        private string getNoLlores()
        {

            var json = string.Empty;

            using (var fs = File.OpenRead("Jsons/NoLlores.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();
            var noLlores = JsonConvert.DeserializeObject<NoLlores>(json);
            Random random = new Random();
            return noLlores.noLlores[random.Next() % noLlores.noLlores.Count()];
        }
    }
}
