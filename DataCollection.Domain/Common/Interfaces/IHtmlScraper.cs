using HtmlAgilityPack;

namespace DataCollection.Domain.Common.Interfaces;

public interface IHtmlScraper
{
    Task<HtmlNodeCollection> GetHtmlTable(HttpClient client, string url);

    string GetColumnA(HtmlNode row, int columnNumber);

    string GetColumn(HtmlNode row, int columnNumber);

    string GetColumnLink(HtmlNode row, int columnNumber);
}
