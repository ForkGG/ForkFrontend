namespace ProjectAveryFrontend.Logic.Services.Connections;

public interface IEntityConnectionService
{
    public Task<bool> SubmitConsoleInAsync(string message);
}