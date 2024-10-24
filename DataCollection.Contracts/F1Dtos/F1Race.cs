namespace DataCollection.Contracts.F1Dtos;

public class F1Race
{
    public int Year { get; set; }

    public int Round { get; set; }

    public string GrandPrixName { get; set; }

    public string CircuitName { get; set; }

    public F1Result[] Results { get; set; }
}
