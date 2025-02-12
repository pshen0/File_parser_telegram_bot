using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Class_Library;
using Main;

namespace Main
{
    public static class Buttons
    {
        /// <summary>
        /// The task for processing received request from buttons.
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task UpdateCall(ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var query = update.CallbackQuery;
            var chat = query.Message.Chat;
            // Processing different buttons.
            switch (query.Data)
            {
                // Getting Csv-file.
                case "button1":
                    {
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Загрузите файл",
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Getting Json-file.
                case "button2":
                    {
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Загрузите файл",
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Choosing sample mode.
                case "button3":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new List<InlineKeyboardButton[]>()
                            {
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("AdmArea", "button6"),
                                },
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("District", "button7"),
                                },
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("AdmArea и пара (Longitude_WGS84, Latitude_WGS84)", "button8"),
                                },
                            });
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Поля для выборки",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Choosing sorting mode. 
                case "button4":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new List<InlineKeyboardButton[]>()
                            {
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("AdmArea по алфавиту в прямом порядке", "button9"),
                                },
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("AdmArea по алфавиту в обратном порядке", "button10"),
                                },
                            });
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Выберите тип сортировки",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Choosing download mode.
                case "button5":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new List<InlineKeyboardButton[]>()
                            {
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Скачать CSV-файл", "button11"),
                                },
                                new InlineKeyboardButton[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Скачать JSON-файл", "button12"),
                                },

                            });
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Выберите формат файла для скачивания",
                            replyMarkup: inlineKeyboard,
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Samling AdmArea.
                case "button6":
                    {
                        SamplingMode.AdmAreaSample = true;
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Введите значение поля AdmArea:",
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Sampling District.
                case "button7":
                    {
                        SamplingMode.DistrictSample = true;
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Введите значение поля District:",
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Sampling AdmArea and (Longitude_WGS84,Latitude_WGS84).
                case "button8":
                    {
                        SamplingMode.AdmAreaLLSample = true;
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Введите поля (AdmArea,Longitude_WGS84,Latitude_WGS84) через запятую без пробелов:",
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Forward sorting.
                case "button9":
                    {
                        SortingMode.FSort = true;
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Отправьте любое сообщение чтобы продолжить",
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Reverse sorting.
                case "button10":

                    {
                        SortingMode.RSort = true;
                        await Client.SendTextMessageAsync(
                            chat.Id,
                            "Отправьте любое сообщение чтобы продолжить",
                            cancellationToken: cancellationToken);
                        return;
                    }
                // Download csv-file.
                case "button11":
                    {
                        CsvProcessing csv = new CsvProcessing();
                        csv.Write(Program.Data);
                        Stream stream = csv.Write(Program.Data);
                        Message message = await Client.SendDocumentAsync(
                            chatId: chat,
                            document: InputFile.FromStream(stream, "new-electrocar-power.csv"),
                            caption: "Результат");

                        return;
                    }
                // Download json-file.
                case "button12":
                    {
                        JsonProcessing js = new JsonProcessing();
                        Stream stream = js.Write(Program.Data);
                        Message message = await Client.SendDocumentAsync(
                            chatId: chat,
                            document: InputFile.FromStream(stream, "new-electrocar-power.json"),
                            caption: "Результат");

                        return;
                    }
            }
            return;
        }
    }
}