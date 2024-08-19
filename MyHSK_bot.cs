using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace MyHSK
{
    class MyHSK_bot
    {
        private TelegramBotClient _bot;
        private CancellationTokenSource _cts;

        public async Task Run()
        {
            var token = Environment.GetEnvironmentVariable("TOKEN") ?? "Your_Bot_Token";
            _cts = new CancellationTokenSource();
            _bot = new TelegramBotClient(token, cancellationToken: _cts.Token);

            var me = await _bot.GetMeAsync();
            await _bot.DropPendingUpdatesAsync();

            _bot.OnError += OnError;
            _bot.OnMessage += OnMessage;

            Console.WriteLine($"@{me.Username} đang được khởi chạy... Bấm escape để tắt");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) ;
            _cts.Cancel();
        }

        private async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine(exception);
            await Task.Delay(2000, _cts.Token);
        }

        private async Task OnMessage(Message msg, UpdateType type)
        {
            if (msg.Text is not { } text)
            {
                Console.WriteLine($"Received a message of type {msg.Type}");
                return;
            }

            if (text.StartsWith('/'))
            {
                await HandleCommand(text, msg);
            }
            else
            {
                await OnTextMessage(msg);
            }
        }

        private async Task HandleCommand(string text, Message msg)
        {
            var space = text.IndexOf(' ');
            if (space < 0) space = text.Length;
            var command = text[..space].ToLower();
            if (command.LastIndexOf('@') is > 0 and int at)
            {
                var me = await _bot.GetMeAsync();
                if (command[(at + 1)..].Equals(me.Username, StringComparison.OrdinalIgnoreCase))
                    command = command[..at];
                else
                    return;
            }
            await OnCommand(command, text[space..].TrimStart(), msg);
        }

        private async Task OnTextMessage(Message msg)
        {
            Console.WriteLine($"Received message '{msg.Text}' in {msg.Chat}");
            await OnCommand("/start", "", msg);
        }

        private async Task OnCommand(string command, string args, Message msg)
        {
            Console.WriteLine($"Received command: {command} {args}");
            switch (command)
            {
                case "/start":
                    await SendStartMessage(msg.Chat);
                    break;
                case "/bike":
                case "/clone":
                case "/cube":
                case "/merge":
                case "/train":
                case "/twerk":
                    await SendGameKeys(command[1..], msg.Chat);
                    break;
            }
        }

        private async Task SendStartMessage(Chat chat)
        {
            await _bot.SendTextMessageAsync(chat, """
            <b><u>Danh sách key của Hamster Kombat</u></b>:
            /bike    - Lấy 4 key của game Bike
            /clone - Lấy 4 key của game Clone
            /cube       - Lấy 4 key của game Cube
            /merge         - Lấy 4 key của game Merge
            /train           - Lấy 4 key của game Train
            /twerk           - Lấy 4 key của game Twerk
            """, parseMode: ParseMode.Html, linkPreviewOptions: true,
                replyMarkup: new ReplyKeyboardRemove());
        }

        private async Task SendGameKeys(string game, Chat chat)
        {
            string filePath = $@"..\..\..\\KeyList\{game}_code.txt";
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    var allLines = System.IO.File.ReadAllLines(filePath).ToList();
                    if (allLines.Count < 1)
                    {
                        await _bot.SendTextMessageAsync(chat, $"Hiện tại code của {game} đã hết");
                        return;
                    }
                    var firstFourLines = allLines.Take(4).ToList();

                    foreach (var codeLine in firstFourLines)
                    {
                        await _bot.SendTextMessageAsync(
                            chatId: chat.Id,
                            text: $"`{codeLine}`",
                            parseMode: ParseMode.Markdown
                        );
                    }

                    allLines.RemoveRange(0, Math.Min(4, allLines.Count));
                    System.IO.File.WriteAllLines(filePath, allLines);
                }
                else
                {
                    await _bot.SendTextMessageAsync(chat, $"File {game}_code.txt không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                await _bot.SendTextMessageAsync(chat, $"Đã xảy ra lỗi khi truy cập file: {ex.Message}");
            }
        }
    }
}
