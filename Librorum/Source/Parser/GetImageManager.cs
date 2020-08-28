using Android.Telecom;
using System;
using System.Collections.Generic;
using System.Text;
using Librorum.Source.Configs;
using HtmlAgilityPack;
using Xamarin.Forms;
using System.Net;
using System.Linq;
using Librorum.Views.CatalogTree;
using Android.Views;

namespace Librorum.Source.Parser
{
    class GetImageManager
    {
        public static List<Uri> Images = new List<Uri>();
        public static List<string> Name = new List<string>();
        public static List<string> Author = new List<string>();
        public static List<string> Pages = new List<string>();
        public static List<string> Description = new List<string>();
        public static string ForwardHref, BackHref, Way;
        public static int i = 1, page = 0;
        static HtmlDocument document = new HtmlDocument();

        public static void DownLoadFileInBackground2(string Path)
        {
            try
            {
                Images.Clear();
                Name.Clear();
                Author.Clear();
                Pages.Clear();
                Description.Clear();
                LoadCurrentPage loadCurrentPage = new LoadCurrentPage();
                Way = "http://loveread.ec/" + Path;
                var pageContent = loadCurrentPage.LoadPage(Way);
                //var document = new HtmlDocument();
                document.LoadHtml(pageContent);

                HtmlNodeCollection ImageLink = document.DocumentNode.SelectNodes(".//p/a/img");
                HtmlNodeCollection NameLink = document.DocumentNode.SelectNodes(".//tr/td/p/a/img");
                HtmlNodeCollection AuthorLink = document.DocumentNode.SelectNodes(".//tr[@class='td_center_color']/td[@class='span_str']/p/a[strong]");
                HtmlNodeCollection DescriptionLink = document.DocumentNode.SelectNodes("//tr[3]/td/p");
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
                foreach (HtmlNode link in DescriptionLink)
                {
                    Description.Add(link.InnerText.Trim());
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
            catch
            {
                BooksInCategory booksInCategory = new BooksInCategory("", "");
                booksInCategory.DisplayAlert("Это первая страница", "Пролистывание назад невозможно. ", "ОK");
            }
        }

        public static string GoForward()
        {
            if (page <= 0 || page >= 6)
            {
                i = 10;
            }
            else
            {
                i = 11;
            }
            HtmlNodeCollection NavigationLink = document.DocumentNode.SelectNodes("//div[@class='navigation']/a[" + i + "]");
            foreach (HtmlNode link in NavigationLink)
            {
                ForwardHref = link.GetAttributeValue("href", "").Replace("amp;", "");
            }
            page++;
            return ForwardHref;
        }

        public static string GoBack()
        {
            if (page <= 0)
            {
                BackHref = Way;
            }
            else
            {
                HtmlNodeCollection NavigationLink = document.DocumentNode.SelectNodes("//div[@class='navigation']/a[1]");
                foreach (HtmlNode link in NavigationLink)
                {
                    BackHref = link.GetAttributeValue("href", "").Replace("amp;", "");
                }
                page--;
            }
            return BackHref;
        }
    }
}
