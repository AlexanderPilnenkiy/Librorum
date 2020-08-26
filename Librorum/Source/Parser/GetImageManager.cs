using Android.Telecom;
using System;
using System.Collections.Generic;
using System.Text;
using Librorum.Source.Configs;
using HtmlAgilityPack;
using Xamarin.Forms;
using System.Net;
using System.Linq;

namespace Librorum.Source.Parser
{
    class GetImageManager
    {
        public static List<Uri> Images = new List<Uri>();
        public static List<string> Name = new List<string>();
        public static List<string> Author = new List<string>();
        public static List<string> Pages = new List<string>();

        public void DownLoadFileInBackground2(string Path)
        {
            Images.Clear();
            Name.Clear();
            Author.Clear();
            Pages.Clear();
            LoadCurrentPage loadCurrentPage = new LoadCurrentPage();
            string Way = "http://loveread.ec/" + Path + "/";
            var pageContent = loadCurrentPage.LoadPage(Way);
            var document = new HtmlDocument();
            document.LoadHtml(pageContent);

            HtmlNodeCollection ImageLink = document.DocumentNode.SelectNodes(".//p/a/img");
            HtmlNodeCollection NameLink = document.DocumentNode.SelectNodes(".//tr/td/p/a/img");
            HtmlNodeCollection AuthorLink = document.DocumentNode.SelectNodes(".//tr[@class='td_center_color']/td[@class='span_str']/p/a[strong]");
            foreach (HtmlNode link in ImageLink)
            {
                Images.Add(new Uri("http://loveread.ec/" + link.GetAttributeValue("src", "")));
            }
            foreach (HtmlNode link in NameLink)
            {
                Name.Add(link.GetAttributeValue("title", ""));
            }
            foreach (HtmlNode link in AuthorLink)
            {
                Author.Add(link.GetAttributeValue("title", ""));
            }
            HtmlNode PagesLink = document.DocumentNode.SelectSingleNode("//td[@class='span_str']");
            HtmlNodeCollection PagesLinkCollection = PagesLink.SelectNodes("//span//following-sibling::text()[8]");
            foreach (HtmlNode aaa in PagesLinkCollection)
            {
                if (aaa.InnerText.Trim().Length > 1 && aaa.InnerText.Trim().Length <= 3)
                {
                    Pages.Add(aaa.InnerText.Trim());
                }
            }
        }
    }
}
