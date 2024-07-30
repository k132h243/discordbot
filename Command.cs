using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DIscordBot
{
    public partial class Command : ModuleBase<SocketCommandContext>
    {
        [Command("도움말")] // Command name.
        [RequireBotPermission(GuildPermission.EmbedLinks)]
        public async Task Help() {
            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(0, 225, 225)
                .WithDescription(
                "?핑 : 핑을 표시합니다.\n" +
                "?주사위(눈, 횟수) : 주사위를 굴립니다.\n" +
                "?스탯정하기, ?stat : T-RPG용 스탯을 결정합니다\n" +
                "?슈슉생성기 : 슈슉슉슉 시발럼아.\n" +
                "?서버정보 : 서버의 정보를 표시합니다.");

            await ReplyAsync(embed: embed.Build());

        }
        [Command("안녕")] // Command name.
        public async Task Hello() {
            await ReplyAsync($@"안녕, {Context.User.Username}!");
        }

        [Command("핑")]
        public async Task Ping() {
            await ReplyAsync($"pong! ({Context.Client.Latency} ms)");
        }

        [Command("슈슉생성기")]
        public async Task Korosu() {
            Random RanNum = new Random();

            string[] array = new string[61];

            for (int i = 0; i < 61; i++) {
                int a = RanNum.Next(7);

                if (a == 1) {
                    array[i] = (".슈");
                }
                else if (a == 2) {
                    array[i] = (".슉");
                }
                else if (a == 3) {
                    array[i] = (".슈슉");
                }
                else if (a == 4) {
                    array[i] = (".시");
                }
                else if (a == 5) {
                    array[i] = (".시발");
                }
                else {
                    array[i] = (".시발럼아");
                }
            }
            string Korosu = String.Join("", array);
            await ReplyAsync($@"슉{Korosu}.시발럼아");
        }

        [Command("선택")]
        public async Task Select(string sel1, string sel2) {

            string result;
            Random RanNum = new Random();
            int a = RanNum.Next(2);
            if (a == 1)
                result = sel1;
            else
                result = sel2;

            EmbedBuilder selector = new EmbedBuilder()
                .WithColor(0, 150, 255)
                .WithTitle("선택결과")
                .WithDescription(result);

            await Context.Channel.SendMessageAsync("", false, selector.Build());  //Embed build


        }
        [Command("서버 정보")]
        [RequireBotPermission(GuildPermission.EmbedLinks)] // Require the bot the have the 'Embed Links' permissions to execute this command.
        public async Task ServerEmbed() {
            double botPercentage = Math.Round(Context.Guild.Users.Count(x => x.IsBot) / Context.Guild.MemberCount * 100d, 2);

            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(0, 225, 225)
                .WithDescription(
                    $"🏷️\n**Guild name:** {Context.Guild.Name}\n" +
                    $"**Created at:** {Context.Guild.CreatedAt:yyy/M/dd}\n" +
                    $"**Owner:** {Context.Guild.Owner}\n\n" +
                    $"💬\n" +
                    $"**Users:** {Context.Guild.MemberCount - Context.Guild.Users.Count(x => x.IsBot)}\n" +
                    $"**Bots:** {Context.Guild.Users.Count(x => x.IsBot)} [ {botPercentage}% ]\n" +
                    $"**Channels:** {Context.Guild.Channels.Count}\n" +
                    $"**Roles:** {Context.Guild.Roles.Count}\n" +
                    $"**Emotes: ** {Context.Guild.Emotes.Count}\n\n" +
                    $"🌎 **Region:** {Context.Guild.VoiceRegionId}\n\n" +
                    $"🔒 **Security level:** {Context.Guild.VerificationLevel}")
                 .WithThumbnailUrl(Context.Guild.IconUrl);

            await ReplyAsync($":information_source: Server info for **{Context.Guild.Name}**", embed: embed.Build());
        }
    }
}
