using System;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Main
{
    /// <summary>
    /// The fields to track the need for sampling.
    /// </summary>
    public static class SamplingMode
    {
        public static bool AdmAreaSample = false;
        public static bool DistrictSample = false;
        public static bool AdmAreaLLSample = false;
    }

    public static class DataSampling
    {
        /// <summary>
        /// The task to sample the data by AdmArea field using LINQ.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SampleAdmArea(string field, ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chat = message.Chat;
            var res = Program.Data.Where(v => v.AdmArea == field).ToList();
            if (res.Count == 0)
            {
                await Client.SendTextMessageAsync(
                    chat.Id,
                    "Значение отсутствует, повторите ввод");
                return;
            }
            SamplingMode.AdmAreaSample = false;
            // Assignment of modified data.
            Program.Data = res;
            await Client.SendTextMessageAsync(
                    chat.Id,
                    "Данные отфильтрованы");
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
            return;
        }
        /// <summary>
        /// The task to sample the data by District field using LINQ.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SampleDistrict(string field, ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chat = message.Chat;
            var res = Program.Data.Where(v => v.District == field).ToList();
            if (res.Count == 0)
            {
                await Client.SendTextMessageAsync(
                    chat.Id,
                    "Значение отсутствует, повторите ввод");

                return;
            }
            SamplingMode.DistrictSample = false;
            // Assignment of modified data.
            Program.Data = res;
            await Client.SendTextMessageAsync(
                    chat.Id,
                    "Данные отфильтрованы");
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
            return;
        }
        /// <summary>
        /// The task to sample the data by (AdmArea,Longitude_WGS84,Latitude_WGS84) field using LINQ.
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <param name="field3"></param>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SampleAdmAreaLL(string field1, string field2, string field3, ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var chat = message.Chat;
            if (!double.TryParse(field2, out double value2) || !double.TryParse(field3, out double value3))
            {
                await Client.SendTextMessageAsync(
                    chat.Id,
                    "Для некоторых полей введены некорректные значения, повторите ввод");
                return;
            }
            var res = Program.Data.Where(v => v.AdmArea == field1 &&
                                         v.Longitude_WGS84 == value2 &&
                                         v.Latitude_WGS84 == value3).ToList();
            if (res.Count == 0)
            {
                await Client.SendTextMessageAsync(
                    chat.Id,
                    "Значение отсутствует, повторите ввод");
                return;
            }
            SamplingMode.AdmAreaLLSample = false;
            // Assignment of modified data.
            Program.Data = res;
            await Client.SendTextMessageAsync(
                    chat.Id,
                    "Данные отфильтрованы");
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
            return;
        }
    }
}