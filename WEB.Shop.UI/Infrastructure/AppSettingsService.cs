using Microsoft.Extensions.Configuration;

namespace WEB.Shop.UI.Infrastructure
{
    public class AppSettingsService
    {
        private readonly IConfiguration _config;

        public AppSettingsService(IConfiguration config)
        {
            _config = config;
        }

        public string GetBaseUrl()
        {
            return _config.GetValue<string>("MySettings:BaseUrl");
        }
    }
}
