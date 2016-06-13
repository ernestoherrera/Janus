using JanusCore.Extensions;
using System.Configuration;


namespace Janus.Support
{
    public class Constants
    {
        #region App Settings
        public static readonly bool DISABLE_LOGIN_AUTHENTICATION = ConfigurationManager.AppSettings["DISABLE_LOGIN_AUTHENTICATION"].ToBool();
        #endregion
    }
}