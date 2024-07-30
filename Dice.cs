using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiceBot
{
    public partial class Command : ModuleBase<SocketCommandContext>
    {   
        [Command("주사위")]
        [Alias("dice")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Dice(int number1, int number2) {
            string dice1;
            int sum1 = 0;

            if (number1 <= 0 || number2 <= 0) {
                dice1 = "0 보다 큰 값을 입력 해 주세요";
            }
            else {
                int[] result = new int[number2];
                for (int i = 0; i < number2; i++) {
                    result[i] = new Random().Next(1, number1 + 1);
                    sum1 += result[i];
                }
                dice1 = $"`{Context.Message.Author.Username}` 님이 주사위를 굴려\n{ String.Join(", ", result)} 이(가) 나왔습니다! (1 - {number1})";
            }


            EmbedBuilder Dice = new EmbedBuilder()
                .WithColor(0, 150, 255)
                .WithTitle($@":game_die: {dice1}")
                .WithDescription($@"(합:{sum1}), {DateTime.Now:t}");

            var channel = Context.Channel as SocketTextChannel;
            var items = await channel.GetMessagesAsync(1).FlattenAsync();
            await channel.DeleteMessagesAsync(items);
            await Context.Channel.SendMessageAsync("", false, Dice.Build());  //Embed build
        }

        [Command("주사위")]
        [Alias("dice")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Dice(int number1) {
            string dice2;
            if (number1 <= 0) {
                dice2 = "0 보다 큰 값을 입력 해 주세요";
            }
            else {
                int result = new Random().Next(1, number1 + 1);
                dice2 = $"`{Context.Message.Author.Username}` 님이 주사위를 굴려\n{result} 이(가) 나왔습니다! (1-{number1})";
            }

            EmbedBuilder Dice = new EmbedBuilder()
                .WithColor(0, 150, 255)
                .WithTitle($@":game_die: {dice2}")
                .WithDescription($@"{DateTime.Now:t}");

            var channel = Context.Channel as SocketTextChannel;
            var items = await channel.GetMessagesAsync(1).FlattenAsync();
            await channel.DeleteMessagesAsync(items);
            await Context.Channel.SendMessageAsync("", false, Dice.Build());  //Embed build 
        }

    }
}
