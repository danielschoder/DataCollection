namespace DataCollection.Application.Interfaces;

public interface IScopedErrorService
{
    List<string> Errors { get; }

    void AddError(string message);

    void AddErrorIf(bool condition, string message);

    T AddNotFoundError<T>(string key) where T : class;
}
