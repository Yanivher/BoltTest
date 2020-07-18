using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoltTest.Controllers
{
    public class GoogleDataProvider : DataProvider
    {
        protected override string BaseUrl => "https://www.google.com/search?q=";
        protected override ProviderType ProviderType => ProviderType.Google;

        protected override List<Item> ParseResult(string html)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            var items = new List<Item>();

            foreach (var node in document.DocumentNode.SelectNodes("//h3[@class='LC20lb DKV0Md']").Take(5))
            {
                var title = HttpUtility.HtmlDecode(node.InnerText);

                items.Add(new Item()
                {
                    Title = title,
                    DataProvider = ProviderType,
                }); ; 
            };

            return items;

        }
    }
}
