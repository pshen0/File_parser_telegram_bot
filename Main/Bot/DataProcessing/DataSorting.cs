using System;
using Main;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Linq;

namespace Main
{
    /// <summary>
    /// The fields to track the need for sorting.
    /// </summary>
    public static class SortingMode
    {
        public static bool FSort = false;
        public static bool RSort = false;
    }

    public class DataSorting
	{
        /// <summary>
        /// Forward sorting the data by AdmArea field using LINQ.
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SortForward(string field, ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chat = message.Chat;
            var res = Program.Data.OrderBy(Data => Data.AdmArea).ToList();
            // Assignment of modified data.
            Program.Data = res;
            await Client.SendTextMessageAsync(chat.Id, "Данные отсортированы");
            var inlineKeyboard = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить CSV-файл", "button11"),
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить JSON-файл", "button12"),
                    },

                });
            await Client.SendTextMessageAsync(
                chat.Id,
                "Выберите действие",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
            SortingMode.FSort = false;
            return;

        }
        /// <summary>
        /// Reverse sorting the data by AdmArea field using LINQ.
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SortReverse(string field, ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chat = message.Chat;
            var res = Program.Data.OrderByDescending(Data => Data.AdmArea).ToList();
            // Assignment of modified data.
            Program.Data = res;
            await Client.SendTextMessageAsync(chat.Id, "Данные отсортированы");
            var inlineKeyboard = new InlineKeyboardMarkup(
                new List<InlineKeyboardButton[]>()
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить CSV-файл", "button11"),
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить JSON-файл", "button12"),
                    },

                });
            await Client.SendTextMessageAsync(
                chat.Id,
                "Выберите действие",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
            SortingMode.RSort = false;
            return;
        }
    }
}

