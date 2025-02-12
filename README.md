# File_parser_telegram_bot
Учебный проект по разработке Telegram-бота на языке C#

## Подготовка к запуску проекта
### Установка .NET SDK
1. [Перейдите на официальную страницу и скачайте .NET 8.0](https://dotnet.microsoft.com/ru-ru/download)
2. Установите скачанный файл, следуя инструкциям на экране
3. После установки вы можете проверить успешность установки, выполнив в консоли команду:
    ```bash
    dotnet --version
    ```

## Установка и использование проекта
1. Клонируйте репозиторий
    ```bash
    git clone https://github.com/pshen0/File_parser_telegram_bot
    ```

2. Откройте папку с репозиторием
    ```bash
    cd path_to_repo/File_parser_telegram_bot
    ```

3. Восстановите зависимости
    ```bash
    dotnet restore
    ```

4. Соберите проект
    ```bash
    dotnet build
    ```

5. Запустите проект
    ```
    dotnet run --project Main/Main.csproj
    ```

6. После запуска проекта откройте Telegram и запустите бот ```@electrocar_power_sazonova_bot```

7. Пример обрабатываемых CSV/JSON файлов можно найти в папке ```File_examples```
