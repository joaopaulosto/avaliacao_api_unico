namespace FeiraSP.WEB.API.CustomLog
{
    public interface IFeiraLog
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}
