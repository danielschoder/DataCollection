namespace DataCollection.Contracts.ExternalServices;

public interface ISlackClient
{
    void SendMessage(string message);
}
