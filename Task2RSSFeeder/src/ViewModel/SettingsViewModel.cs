namespace Task2RSSFeeder.ViewModel
{
    public class SettingsViewModel
    {
        public int UpdateRate { get => Settings.UpdateRate;
            set => Settings.UpdateRate = value;
        }
        public string RssURL { get => Settings.RssURL;
            set => Settings.RssURL = value;
        }
    }
}