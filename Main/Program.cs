using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

//Для запуска бота отправьте сообщение start
//@electrocar_power_sazonova_bot

namespace Main
{
    class Program
    {
        internal static  List<Class_Library.ElectrocarPower> Data;
        private static ITelegramBotClient BotClient;
        private static ReceiverOptions ReceiverOption;

        static async Task Main()
        {
            // Creating a client of telegrambot.
            BotClient = new TelegramBotClient("7009900392:AAHtyBiPNaRKxVo9Iui3YpGGMkkseo0aiW4");
            ReceiverOption = new ReceiverOptions{
                AllowedUpdates = new[]
                {
                UpdateType.Message,
                UpdateType.CallbackQuery
                }
            };
            var _token = new CancellationTokenSource();
            // Launch the bot.
            BotClient.StartReceiving(Handlers.Update, Handlers.Error, ReceiverOption, _token.Token);
            var _bot = await BotClient.GetMeAsync();
            Console.ReadKey();
        }
    }
}
