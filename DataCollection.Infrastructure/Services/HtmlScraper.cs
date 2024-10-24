using DataCollection.Domain.Common.Interfaces;
using HtmlAgilityPack;

namespace DataCollection.Infrastructure.Services;

public class HtmlScraper : IHtmlScraper
{
    public async Task<HtmlNodeCollection> GetHtmlTable(HttpClient client, string url)
    {
        var response = await client.GetAsync(url);
        var htmlContent = await response.Content.ReadAsStringAsync();
        var document = new HtmlDocument();
        document.LoadHtml(htmlContent);
        return document.DocumentNode.SelectNodes("//tbody/tr");
    }

    public string GetColumn(HtmlNode row, int columnNumber)
        => row.SelectSingleNode($".//td[{columnNumber}]/p").InnerText.Trim();

    public string GetColumnA(HtmlNode row, int columnNumber)
        => row.SelectSingleNode($".//td[{columnNumber}]/p/a").InnerText.Trim();

    public string GetColumnLink(HtmlNode row, int columnNumber)
        => row.SelectSingleNode($".//td[{columnNumber}]/p/a").Attributes["href"].Value;
}
