namespace DataCollection.Contracts.Responses;

public class SearchResponse
{
    public string Type { get; set; }

    public int FromYear { get; set; }

    public int ToYear { get; set; }

    public int RowsFound { get; set; }

    public SearchResponse(string type, int fromYear, int toYear, int rowsFound)
        => (Type, FromYear, ToYear) = (type, fromYear, toYear);

    public SearchResponse(string type, int year, int rowsFound)
        : this(type, year, year, rowsFound) { }
}
