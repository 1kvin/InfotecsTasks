using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Windows;
using System.Xml;
using Task2RSS.Model;

namespace Task2RSSFeeder
{
    public static class RSSFeedReader
    {
        public static List<RSSItemModel> Read()
        {
            var list = new List<RSSItemModel>();
            try
            {
                using (var reader = XmlReader.Create(Settings.RssURL))
                {
                    var formatter = new Rss20FeedFormatter();
                    formatter.ReadFrom(reader);
                    var items = formatter.Feed.Items;
                    list.AddRange(items.Select(item => new RSSItemModel
                    {
                        Title = item.Title.Text, PublishDate = item.PublishDate.DateTime,
                        Description = item.Summary.Text, URL = item.Links[0].Uri.AbsoluteUri
                    }));
                }

                return list;
            }
            catch
            {
                MessageBox.Show("Ошибка загрузки RSS ленты.", "Ошибка", MessageBoxButton.OK);
                return null;
            }
        }
    }
}