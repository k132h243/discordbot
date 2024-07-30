using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;

namespace DiscordBot
{
	public class Program
	{
        DiscordSocketClient _client;
        CommandService _commands;

        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();



        public async Task MainAsync() {

            Console.WriteLine("Ready for takeoff...");

            _client = new DiscordSocketClient(new DiscordSocketConfig() {    //디스코드 봇 초기화
                LogLevel = LogSeverity.Verbose                              //봇의 로그 레벨 설정 
            });
            _commands = new CommandService(new CommandServiceConfig()        //명령어 수신 클라이언트 초기화
            {
                LogLevel = LogSeverity.Verbose                              //봇의 로그 레벨 설정
            });

            //로그 수신 시 로그 출력 함수에서 출력되도록 설정
            _client.Log += Log;
            _commands.Log += Log;

            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;
            var token = File.ReadAllText("token.txt");

            // Log in to Discord and start the bot.
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);  //봇에 명령어 모듈 등록

            _client.MessageReceived += CommandHandler;

            // Run the bot forever.
            await Task.Delay(-1);
        }


        
        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


        private async Task CommandHandler(SocketMessage message) {
            //수신한 메시지가 사용자가 보낸 게 아닐 때 취소
            var msg = message as SocketUserMessage;
            if (msg == null)
                return;

            int pos = 0;

            if (!msg.HasCharPrefix('/',ref pos))  // prefix
                return;

            if (msg.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, msg);  //수신된 메시지에 대한 컨텍스트 생성   
            var result = await _commands.ExecuteAsync(context: context,argPos:pos,services: null);  //모듈이 명령어를 처리하게 설정
        }
    }
}
