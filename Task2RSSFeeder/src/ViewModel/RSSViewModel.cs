using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.ServiceModel.Syndication;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using Task2RSS.Model;
using Task2RSSFeeder;
using Task2RSSFeeder.View;
using System.Timers;

namespace Task2RSS.ViewModel
{
    public class RSSViewModel
    {
        public List<RSSItemModel> RSSItems = new List<RSSItemModel>();
        private readonly MainWindow mainWindow;
        private readonly Timer timer;
        private RSSItemModel selectedItem;
        private Command openInBrowserCommand;
        private Command openSettingsCommand;
        public RSSItemModel SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                UpdateText();
                mainWindow.OpenInBrowser.Visibility = Visibility.Visible;
            }
        }

        private void UpdateText()
        {
            var textBuilder = new StringBuilder("<head>");
            textBuilder.Append("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>");
            textBuilder.Append("</head> <body>");
            textBuilder.Append(selectedItem?.Description);
            textBuilder.Append("</body>");
            mainWindow.webBrowser.NavigateToString(textBuilder.ToString());
        }

        public ICommand SettingsButton
        {
            get
            {
                return openSettingsCommand ?? (openSettingsCommand = new Command(OpenSettings, () => CanExecute));
            }
        }

        public ICommand OpenInBrowserButton
        {
            get
            {
                return openInBrowserCommand ?? (openInBrowserCommand = new Command(OpenURL, () => CanExecute));
            }
        }

        private bool CanExecute => true;

        private void OpenURL()
        {
            System.Diagnostics.Process.Start(selectedItem.URL);
        }
        private void OpenSettings()
        {
            var settingsWindow = new SettingsWindow {Owner = mainWindow};
            settingsWindow.ShowDialog();
            Update();
        }
        public RSSViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            timer = new Timer(Settings.UpdateRate * 1000 * 60);
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.Enabled = true;
            Update();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            mainWindow.ListBox.Dispatcher.BeginInvoke(new Action(Update));
        }
        private void Update()
        {
            timer.Interval = Settings.UpdateRate * 1000 * 60;
            var readRss = RSSFeedReader.Read();
            if (Equals(mainWindow.ListBox.ItemsSource, readRss)) return;
            mainWindow.ListBox.ItemsSource = RSSFeedReader.Read();
            mainWindow.ListBox.Items.Refresh();
        } 
    }
}
