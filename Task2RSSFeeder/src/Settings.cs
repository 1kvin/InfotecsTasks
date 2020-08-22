using System;
using System.Windows;
using System.Xml;

namespace Task2RSSFeeder
{
    public static class Settings
    {
        private static int updateRate = 10;
        private static string rssURL = "https://habr.com/rss/interesting/";

        public static string RssURL
        {
            get => rssURL;
            set
            {
                rssURL = value;
                Save();
            }
        }

        public static int UpdateRate
        {
            get => updateRate;
            set
            {
                if (value <= 0) throw new ArgumentException("UpdateRate can be only > 0");
                updateRate = value;
                Save();
            }
        }

        static Settings()
        {
            var config = new XmlDocument();
            config.Load("config.xml");
            var xRoot = config.DocumentElement;
            foreach (XmlNode child in xRoot.ChildNodes)
            {
                switch (child.Name)
                {
                    case "url":
                        RssURL = child.InnerText;
                        break;
                    case "updateRate":
                        if (!UpdateRateValidCheck(child.InnerText, out updateRate))
                            ShowErrorMessage();
                        break;
                }
            }
        }

        private static bool UpdateRateValidCheck(string rate, out int result)
        {
            result = updateRate;
            if (string.IsNullOrEmpty(rate))
                return false;
            if (int.TryParse(rate, out var val))
            {
                if (val <= 0)
                {
                    return false;
                }
                else
                {
                    result = val;
                }
            }
            else
                return false;

            return true;
        }

        private static void Save()
        {
            var config = new XmlDocument();
            config.Load("config.xml");
            var xRoot = config.DocumentElement;
            foreach (XmlNode child in xRoot.ChildNodes)
            {
                switch (child.Name)
                {
                    case "url":
                        xRoot.RemoveChild(child);
                        var newUrl = config.CreateElement("url");
                        newUrl.InnerText = rssURL;
                        config.DocumentElement.AppendChild(newUrl);
                        break;
                    case "updateRate":
                        xRoot.RemoveChild(child);
                        var newUpdateRate = config.CreateElement("updateRate");
                        newUpdateRate.InnerText = updateRate.ToString();
                        config.DocumentElement.AppendChild(newUpdateRate);
                        break;
                }
            }

            config.Save("config.xml");
        }

        private static void ShowErrorMessage()
        {
            MessageBox.Show("Ошибка загрузки настроек. Будут использованы настройки по умолчанию.", "Ошибка",
                MessageBoxButton.OK);
        }
    }
}