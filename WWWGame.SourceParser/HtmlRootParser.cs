using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WWWGame.LogicLayer.Model;
using WWWGame.LogicLayer.Parser;

namespace WWWGame.SourceParser
{
    public class HtmlRootParser : IRootParser
    {
        public async Task<IEnumerable<Tourn>> GetTourns(string url)
        {
            HttpClient client = new HttpClient();
            var doc = new HtmlAgilityPack.HtmlDocument();
            var html = await client.GetStringAsync(url);
            doc.LoadHtml(html);

            //var tcs = new TaskCompletionSource<HtmlDocument>();
            //HtmlWeb web = new HtmlWeb();
            //web.LoadCompleted += (s, e) =>
            //{
            //    if (e.Error != null) tcs.TrySetException(e.Error);
            //    else tcs.TrySetResult(e.Document);
            //};

            //HtmlDocument doc = await web.LoadAsync(url);

            var tourns = new List<Tourn>();

            var mainDiv = doc.GetElementbyId("main");

            var ul = mainDiv.ChildNodes.FindFirst("ul");

            var list = ul.ChildNodes.Where(el => el.Name == "li").ToList();

            foreach (var node in list)
            {
                var link = node.SelectSingleNode(".//a");
                string href = link.GetAttributeValue("href", string.Empty);
                tourns.Add(new Tourn()
                {
                    Url = href,
                    Title = HttpUtility.HtmlDecode(link.InnerText)
                });
            }

            return tourns;
        }
    }
}