using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Class_Library;
using System.Collections.Generic;
namespace Main
{
    public static class UpdateMessage
    {
        /// <summary>
        /// The task processes text messages received through the bot.
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task UpdateText(ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            // Output the text of the message.
            Console.WriteLine($"{message.From.FirstName}  |  {message.Text}");
            var chat = message.Chat;
            if (message.Text == "start" || message.Text == "/start")
            {
                var inlineKeyboard = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>()
                    {
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Загрузить CSV-файл", "button1"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Загрузить JSON-файл", "button2"),
                        },
                    });
                await Client.SendTextMessageAsync(
                    chat.Id,
                    "Выберите действие",
                    replyMarkup: inlineKeyboard);
                return;
            }
            // Calling the sample.
            if (SamplingMode.AdmAreaSample == true)
            {
                await DataSampling.SampleAdmArea(message.Text, Client, update, cancellationToken);
            }
            if (SamplingMode.DistrictSample == true)
            {
                await DataSampling.SampleDistrict(message.Text, Client, update, cancellationToken);
            }
            if (SamplingMode.AdmAreaLLSample == true)
            {
                try
                {
                    string field1 = message.Text.Split(",")[0];
                    string field2 = message.Text.Split(",")[1];
                    string field3 = message.Text.Split(",")[2];
                    await DataSampling.SampleAdmAreaLL(field1, field2, field3, Client, update, cancellationToken);
                }
                catch (ArgumentException ex)
                {
                    await Client.SendTextMessageAsync(
                    chat.Id,
                    "Введены некорректные значения, потворите ввод");
                    return;
                }
            }
            // Calling the sorting.
            if (SortingMode.FSort == true)
            {
                await DataSorting.SortForward(message.Text, Client, update, cancellationToken);
            }
            if (SortingMode.RSort == true)
            {
                await DataSorting.SortReverse(message.Text, Client, update, cancellationToken);
            }
        }
        /// <summary>
        /// The task processes document messages received through the bot.
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task UpdateDocument(ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var ftype = message.Document.MimeType ?? "";
            var chat = message.Chat;
            // Checking data type correctness.
            if (ftype != "text/csv" && ftype != "application/json")
            {
                await Client.SendTextMessageAsync(
                    chat.Id,
                    "Формат не соотвествует CSV или JSON, потворите загрузку");
                return;
            }
            else
            {
                var fid = update.Message.Document.FileId;
                var finfo = await Client.GetFileAsync(fid);
                string filePath = finfo.FilePath;
                // Csv-file processing.
                if (ftype == "text/csv")
                {
                    CsvProcessing csv = new CsvProcessing();
                    using (Stream fileStream = System.IO.File.Create("csv_input.csv"))
                    {
                        var file = await Client.GetInfoAndDownloadFileAsync(fid, fileStream, cancellationToken);
                    }
                    using (FileStream f = new FileStream("csv_input.csv", FileMode.Open, System.IO.FileAccess.Read))
                    {
                        Program.Data = csv.Read(f);
                    }
                    var inlineKeyboard = new InlineKeyboardMarkup(
                                        new List<InlineKeyboardButton[]>()
                                        {
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести выборку", "button3"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести сортировку", "button4"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Скачать файл", "button5"),
                        },
                                        });
                    await Client.SendTextMessageAsync(
                        chat.Id,
                        "Данные из файла успешно считаны",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);
                }
                // Json-file processing.
                if (ftype == "application/json")
                {
                    JsonProcessing json = new JsonProcessing();
                    using (Stream fileStream = new MemoryStream())
                    {
                        var file = await Client.GetInfoAndDownloadFileAsync(fid, fileStream, cancellationToken);
                        Program.Data = json.Read(fileStream);
                    }
                    var inlineKeyboard = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>()
                    {
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести выборку", "button3"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести сортировку", "button4"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Скачать файл", "button5"),
                        },
                    });

                    await Client.SendTextMessageAsync(
                        chat.Id,
                        "Данные из файла успешно считаны",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);
                }
            }
        }
    }
}