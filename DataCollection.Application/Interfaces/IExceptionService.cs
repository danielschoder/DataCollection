namespace DataCollection.Application.Interfaces;

public interface IExceptionService
{
    Task HandleExceptionAsync(Exception exception);
}
