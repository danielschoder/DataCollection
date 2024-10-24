using DataCollection.Contracts.ExternalServices;
using DataCollection.Contracts.F1Dtos;
using DataCollection.Domain.Common.Interfaces;
using System.Net;
using System.Text.RegularExpressions;

namespace DataCollection.Infrastructure.ExternalApis;

public class F1Client(HttpClient client, IHtmlScraper htmlScraper) : IF1Client
{
    private readonly HttpClient _client = client;
    private readonly IHtmlScraper _htmlScraper = htmlScraper;

    public async Task<List<F1Constructor>> GetConstructorsAsync(int year)
        => (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/team"))
            .Select(row => new F1Constructor { Name = _htmlScraper.GetColumnA(row, 2) })
            .ToList();

    public async Task<List<F1Driver>> GetDriversAsync(int year)
        => (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/drivers"))
            .Select(row => new F1Driver { Name = _htmlScraper.GetColumnA(row, 2) })
            .ToList();

    public async Task<F1Race[]> GetRacesAsync(int year)
        => (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/races"))
            .Select((row, index) => new F1Race
            {
                Year = year,
                Round = index + 1,
                GrandPrixName = _htmlScraper.GetColumnA(row, 1)
            })
            .ToArray();

    public async Task<F1Race> GetRaceResultsAsync(int year, int round)
    {
        var raceHtmlNode = (await _htmlScraper.GetHtmlTable(_client, $"/en/results/{year}/races"))[round - 1];
        return new F1Race
        {
            GrandPrixName = _htmlScraper.GetColumnA(raceHtmlNode, 1),
            Year = year,
            Round = round,
            Results = await GetResultsAsync(_htmlScraper.GetColumnLink(raceHtmlNode, 1))
        };

        async Task<F1Result[]> GetResultsAsync(string url)
            => (await _htmlScraper.GetHtmlTable(_client, $"en/results/2024/{url}"))
                .Select(row => new F1Result
                {
                    Position = _htmlScraper.GetColumn(row, 1),
                    DriverName = FormatDriverName(_htmlScraper.GetColumn(row, 3)),
                    ConstructorName = _htmlScraper.GetColumn(row, 4),
                    Points = int.Parse(_htmlScraper.GetColumn(row, 7)),
                })
                .ToArray();

    }

    private string FormatDriverName(string name)
    {
        name = Regex.Replace(WebUtility.HtmlDecode(name).Trim(), @"\s+", " ");
        return name.Length >= 3 && name[^3..].All(char.IsUpper)
            ? name.Substring(0, name.Length - 3)
            : name;
    }
}
