namespace DataCollection.Contracts.F1Dtos;

public class F1Data
{
    public F1Constructor[] Constructors { get; set; }

    public F1Driver[] Drivers { get; set; }

    public F1Race[] Races { get; set; }

    public F1Season[] Seasons { get; set; }
}
