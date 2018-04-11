using System.Configuration;

namespace LNU.JAVA
{
    public static class Settings
    {
        public static string WebServiceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["WebService.WebApiUrl"];
            }
        }
    }
}
