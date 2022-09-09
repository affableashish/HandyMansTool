namespace HMT.Web.Server.Interfaces
{
    public interface IAppLoggerService<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}
