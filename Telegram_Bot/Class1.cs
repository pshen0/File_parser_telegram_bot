using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using FilesLibrary;
using System.Collections.Generic;

namespace Bot
{
    public static class UpdateTypesMessage
    {
        public static async Task UpdateTypeMessageText(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            var user = message.From;
            Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");

            var chat = message.Chat;

            if (message.Text == "/start")
            {
                var inlineKeyboard = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>() // Cодрежит в себе массив из класса кнопок.
                    {
                        new InlineKeyboardButton[] // тут создаем массив кнопок
                        {
                            InlineKeyboardButton.WithCallbackData("Загрузить CSV файл на обработку", "button1"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Загрузить JSON файл на обработку", "button2"),
                        },
                    });
                await botClient.SendTextMessageAsync(
                    chat.Id,
                    "Выберите опцию:",
                    replyMarkup: inlineKeyboard);
                return;
            }

            if (FlagsForSample.startListenMessageSampleCovArea == true)
            {
                await Sample.SampleCoverageArea(message.Text, botClient, update, cancellationToken);
            }
            if (FlagsForSample.startListenMessageSampleWifiName == true)
            {
                await Sample.SampleWifiName(message.Text, botClient, update, cancellationToken);
            }
            if (FlagsForSample.startListenMessageSampleDistrictAccessFlag == true)
            {
                try
                {
                    var firstVal = message.Text.Split(",")[0];
                    var secondVal = message.Text.Split(",")[1];

                    await Sample.SampleDistrictAndAccessFlag(firstVal, secondVal, botClient, update, cancellationToken);
                }
                catch (ArgumentException ex)
                {
                    await botClient.SendTextMessageAsync(
                    chat.Id,
                    "Значения введены в неверном формате! Потворите ввод:");

                    return;
                }
            }
        }
        public static async Task UpdateTypeMessageDocument(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var fileCheck = update.Message.Document.MimeType ?? "";
            var message = update.Message;
            var chat = message.Chat;

            if (fileCheck != "text/csv" && fileCheck != "application/json")
            {
                await botClient.SendTextMessageAsync(
                    chat.Id,
                    "Формат не соотвествует ни Csv, ни json. Загрузите снова:");

                return;
            }

            else
            {
                var fileId = update.Message.Document.FileId;
                var fileInfo = await botClient.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;

                if (fileCheck == "text/csv")
                {
                    CSVProcessing csv = new CSVProcessing();

                    using (Stream fileStream = new MemoryStream())
                    {
                        var file = await botClient.GetInfoAndDownloadFileAsync(fileId, fileStream, cancellationToken);
                        Program.list = csv.Read(fileStream);
                    }
                    var inlineKeyboard = new InlineKeyboardMarkup(
                                        new List<InlineKeyboardButton[]>() // Cодрежит в себе массив из класса кнопок.
                                        {
                        new InlineKeyboardButton[] // тут создаем массив кнопок
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести сортировку по одному из полей", "button3"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести выборку по одному из полей", "button4"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Скачать файл", "button5"),
                        },
                                        });

                    await botClient.SendTextMessageAsync(
                        chat.Id,
                        "Данные успешно прочитаны!",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);

                }

                if (fileCheck == "application/json")
                {
                    JSONProcessing js = new JSONProcessing();

                    using (Stream fileStream = new MemoryStream())
                    {
                        var file = await botClient.GetInfoAndDownloadFileAsync(fileId, fileStream, cancellationToken);
                        Program.list = js.Read(fileStream);
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(
                    new List<InlineKeyboardButton[]>() // Cодрежит в себе массив из класса кнопок.
                    {
                        new InlineKeyboardButton[] // тут создаем массив кнопок
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести сортировку по одному из полей", "button3"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Произвести выборку по одному из полей", "button4"),
                        },
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("Скачать файл", "button5"),
                        },
                    });

                    await botClient.SendTextMessageAsync(
                        chat.Id,
                        "Данные успешно прочитаны!",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);
                }
            }
        }
    }
}