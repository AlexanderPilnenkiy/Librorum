using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Librorum.Source.Parser
{
    class GetInfoManager
    {
        public static Dictionary<string, string> _Info = new Dictionary<string, string>();

        public Dictionary<string, string> Info(string Way, string Library)
        {
            _Info.Clear();
            LoadCurrentPage loadCurrentPage = new LoadCurrentPage();
            var pageContent = loadCurrentPage.LoadPage(Library);
            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(pageContent);
            HtmlNodeCollection links = document.DocumentNode.SelectNodes(Way);
            foreach (HtmlNode link in links)
                _Info.Add(link.InnerText, link.GetAttributeValue("href", ""));
            return _Info;
        }
    }
}
