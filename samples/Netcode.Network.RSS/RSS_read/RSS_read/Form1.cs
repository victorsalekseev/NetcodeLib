using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Netcode.Network.RSS.Rss;
using Netcode.Network.RSS.UI;

namespace RSS_read
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.DocumentText = "<HTML><HEAD><TITLE></TITLE></HEAD><BODY>" + LoadRssFeed() + "</BODY></HTML>";
        }


        private RssFeed rssFeed;
        private ItemListView<RssItem> rssView;
        private string LoadRssFeed()
        {
            rssFeed = RssFeed.FromUri(@"http://votpusk.ru/newsrss/cn/newsfi.xml");
            rssView = new ItemListView<RssItem>(rssFeed.MainChannel.Title, rssFeed.MainChannel.Items);
            return LastRSS();
        }

        private string LastRSS()
        {
            string urlart = string.Empty;
            urlart += "<table align=\"left\" valign=\"top\" width=\"141\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td  align=\"left\" valign=\"top\">";
            for (int i = 0; i < Math.Min(rssFeed.MainChannel.Items.Count, 5); i++)
            {
                urlart += ("<a href=\"" + rssFeed.MainChannel.Items[i].Link + "\" style='text-decoration:none;font-size:10pt;' target='_blank'>" + rssFeed.MainChannel.Items[i].Title + "</a><br><br> ");
            }
            urlart += "</td></tr></table>";
            return urlart;
        }
    }

}