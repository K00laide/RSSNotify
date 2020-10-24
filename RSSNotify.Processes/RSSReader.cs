using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Linq;

namespace RSSNotify.Processes
{
    public class RSSReader
    {
        private string _feedUri;

        public RSSReader(string feedUri)
        {
            _feedUri = feedUri;
        }

        public List<SyndicationItem> GetFeedItems()
        {
            using var reader = XmlReader.Create(_feedUri);
            var feed = SyndicationFeed.Load(reader);
            return feed.Items.ToList();
        }
    }
}
