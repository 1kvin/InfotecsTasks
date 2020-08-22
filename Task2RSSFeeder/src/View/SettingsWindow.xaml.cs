using System.Text.RegularExpressions;
using System.Windows;
using Task2RSS.ViewModel;
using Task2RSSFeeder.ViewModel;

namespace Task2RSSFeeder.View
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = new SettingsViewModel();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }
    }
}