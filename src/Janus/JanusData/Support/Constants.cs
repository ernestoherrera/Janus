using JanusCore.Extensions;
using System.Configuration;

namespace JanusData.Support
{
    public class Constants
    {
        #region Connection Strings
        public static readonly string CONNECTION_NAME = "FluentSql";
        #endregion

        #region App Settings
        public static readonly bool DISABLE_LOGIN_AUTHENTICATION = ConfigurationManager.AppSettings["DISABLE_LOGIN_AUTHENTICATION"].ToBool();
        #endregion
    }
}
