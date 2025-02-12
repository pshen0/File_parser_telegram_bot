using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Main
{
    public static class Handlers
    {
        /// <summary>
        /// The task for processing updates that come from the user.
        /// </summary>
        /// <param name="Client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task Update(ITelegramBotClient Client, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    // Message updating.
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            Console.WriteLine($"{message.From.FirstName}  |  {message.Text}");
                            switch (message.Type)
                            {
                                // Text messages updating.
                                case MessageType.Text:
                                    {
                                        await UpdateMessage.UpdateText(Client, update, cancellationToken);
                                        return;
                                    }
                                // Document messages updating.
                                case MessageType.Document:
                                    {
                                        await UpdateMessage.UpdateDocument(Client, update, cancellationToken);

                                        return;
                                    }
                            }
                            return;
                        }
                    // Callback Query updating.
                    case UpdateType.CallbackQuery:
                        {
                            await Buttons.UpdateCall(Client, update, cancellationToken);
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /// <summary>
        /// The task for errors handling.
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="error"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task Error(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            // Output the message about an error.
            string Error = error switch
            {
                ApiRequestException apiRequestException
                    => $"TelegramError:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                  _ => error.ToString()
            };
            Console.WriteLine(Error);
            return Task.CompletedTask;
        }
    }
}

