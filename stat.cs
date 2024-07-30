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
        int STR;
        int CON;
        int SIZ;
        int DEX;
        int APP;
        int INT;
        int POW;
        int EDU;
        int MOV;

        [Command("스탯정하기")] // Command name.
        [Alias("스탯 정하기", "stat", "스탯결정", "스탯")]
        [RequireBotPermission(GuildPermission.EmbedLinks)]
        public async Task Stat() {
            

            STR = Stat1();
            CON = Stat1();
            POW = Stat1();
            DEX = Stat1();
            APP = Stat1();

            SIZ = Stat2();
            INT = Stat2();
            EDU = Stat2();

            MOV = MOVE();
            


            EmbedBuilder embed = new EmbedBuilder();
            embed.Color = Color.Blue;          // 메시지의 색을 파란색으로 설정
            embed.Title = "스탯 결정";          //Embed의 제목
            embed.Description = $"`{Context.Message.Author.Username}`님의 스탯";    //Embed의 설명

            embed.AddField("근력 (3D6)×5", $"{STR}", true);
            embed.AddField("건강 (3D6)×5", $"{CON}", true);
            embed.AddField("크기 [(2D6)+6]×5", $"{SIZ}", true);
            embed.AddField("민첩 (3D6)×5", $"{DEX}", true);
            embed.AddField("외모 (3D6)×5", $"{APP}", true);
            embed.AddField("지능 [(2D6)+6]×5", $"{INT}", true);
            embed.AddField("정신 (3D6)×5", $"{POW}", true);
            embed.AddField("교육 [(2D6)+6]×5", $"{EDU}", true);
            embed.AddField("이동력", $"{MOV}", true);


            await Context.Channel.SendMessageAsync("", false, embed.Build());  //Embed를 빌드하여 메시지 전송
        }

        public int Stat1() {
            int i;
            Random dice = new Random();
            i = dice.Next(3, 19);
            return i * 5;
        }

        public int Stat2() {
            int i;
            Random dice = new Random();
            i = dice.Next(2, 13);
            return (i + 6) * 5;
        }

        public int MOVE() {
            if (DEX < SIZ && STR < SIZ) {
                return 7;
            }
            else if ((DEX == SIZ && STR == SIZ) || DEX <= SIZ || STR <= SIZ) {
                return 8;
            }
            else if(DEX > SIZ && STR > SIZ) {
                return 9;
            }
            else {
                return 0;
            }
        }
    }
}

