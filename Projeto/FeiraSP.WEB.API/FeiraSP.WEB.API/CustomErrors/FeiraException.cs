using System.Globalization;

namespace FeiraSP.WEB.API.CustomErrors
{
    public class FeiraException: Exception
    {
        public FeiraException() : base() { }

        public FeiraException(string message) : base(message) { }

        public FeiraException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
