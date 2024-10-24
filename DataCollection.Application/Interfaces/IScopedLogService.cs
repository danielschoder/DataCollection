using System.Runtime.CompilerServices;

namespace DataCollection.Application.Interfaces;

public interface IScopedLogService
{
    void Log(
        string content = "",
        string var = "",
        [CallerMemberName] string callerMethod = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0);

    List<string> GetLogsAsList();

    string ExceptionAsTextBlock(Exception exception);

    string GetLogsAsString(string title);

    void AddText(string text);
}
