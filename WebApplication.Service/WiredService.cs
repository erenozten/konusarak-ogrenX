using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;
using WebApplication.Service.DTOs;

namespace WebApplication.Service
{
    public class WiredService
    {
        public List<WiredArticleDto> DownloadRecentArticles(int count)
        {
            var result = new List<WiredArticleDto>();
            List<string> articleUrls = DownloadUrls().Take(count).ToList();

            WebClient webClient = new WebClient();
            foreach (string articleUrl in articleUrls)
            {
                byte[] data = webClient.DownloadData(articleUrl);
                string html = Encoding.UTF8.GetString(data);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                HtmlNode h1 = doc.DocumentNode.SelectSingleNode("//h1");

                HtmlNode article =
                    doc.DocumentNode.SelectSingleNode(
                        "//div[@class='article__chunks']");

                if (article != null)
                {
                    result.Add(new WiredArticleDto()
                    {
                        Title = HttpUtility.HtmlDecode(h1.InnerText),
                        Content = HttpUtility.HtmlDecode(article.InnerText)
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// En son eklenen haberlerin linklerini indirir
        /// </summary>
        /// <returns></returns>
        private List<string> DownloadUrls()
        {
            using (var webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData("https://wired.com");
                string html = Encoding.UTF8.GetString(data);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var div = doc.DocumentNode.SelectSingleNode(
                    "//div[@class='post-listing-component  post-listing-component--with-border']");

                var ul = div.Descendants("ul").First();
                var liElements = ul.Descendants("li");

                List<string> urls = new List<string>();
                foreach (var liElement in liElements)
                {
                    var item = liElement.Descendants("a").First();

                    var relativeUrl = item.GetAttributeValue("href", "");
                    var absoluteUrl = "https://wired.com" + relativeUrl;

                    urls.Add(absoluteUrl);
                }
                return urls;
            }
        }
    }
}