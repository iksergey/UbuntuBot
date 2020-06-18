namespace UbuntuBot
{
    static class SettingsApp
    {
        static SettingsApp()
        {
            Urls = new string[]
            {
                "http://ya.ru/",
                "http://vk.com/",
                "http://google.com/",
                "https://telegram.org/",
                "https://api.telegram.org/"
            };

            Token = "{token}";
          

            ProxyUserName = "";
            ProxyPassword = "";
            ProxyHost = "http://80.187.140.26";
            ProxyPort = "8080";

        }

        static public string[] Urls { get; private set; }
        public static string Token { get; private set; }
        static public string ProxyUserName { get; private set; }
        static public string ProxyPassword { get; private set; }
        static public string ProxyHost { get; private set; }
        static public string ProxyPort { get; private set; }

    }
}
