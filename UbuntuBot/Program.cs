using System;
using System.Net;
using System.Net.Http;
using Telegram.Bot;

namespace UbuntuBot
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Start Application");

            #region test

            foreach (var url in SettingsApp.Urls)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Console.WriteLine($"{(int)response.StatusCode,5} {response.StatusCode,10} {url}");
                    response.Close();                    
                }
                catch  
                {
                    Console.WriteLine($"no response - {url}");
                }
            }

            var proxy = new WebProxy
            {
                Address = new Uri($"{SettingsApp.ProxyHost}:{SettingsApp.ProxyPort}"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,

                //Credentials = new NetworkCredential(
                //    userName: SettingsApp.ProxyUserName,
                //    password: SettingsApp.ProxyPassword
                //    )
            };

            HttpClient httpClient = new HttpClient(new HttpClientHandler { Proxy = proxy });

            #endregion

            TelegramBotClient bot = new TelegramBotClient(SettingsApp.Token, httpClient);

            var botName = bot.GetMeAsync().Result.FirstName;

            Console.WriteLine($"{botName} start...");

            bot.OnMessage += (s, args) =>
            {
                string messageText = args.Message.Text ?? "null";
                string userName = args.Message.Chat.FirstName;
                long userId = args.Message.Chat.Id;

                var response = String.Empty;

                switch (messageText.ToLower())
                {
                    case "/os": response = $"OSVersion: {Environment.OSVersion}"; break;
                    case "/me": response = $"{botName}"; break;
                    default: response = "/os\n/me"; break;
                }
                bot.SendTextMessageAsync(
                           chatId: userId,
                           text: response ,
                           replyToMessageId: args.Message.MessageId
                           );
            };

            bot.StartReceiving();

            Console.ReadLine();

        }
    }
}
