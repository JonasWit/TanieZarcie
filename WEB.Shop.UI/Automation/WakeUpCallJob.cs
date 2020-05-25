using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WEB.Shop.UI.Automation
{
    class WakeUpCallJob : IJob
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;

        public WakeUpCallJob(IHttpClientFactory httpFactory, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _configuration = configuration;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using var client = _httpFactory.CreateClient();

            var response = client.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri(uriString: $"{_configuration["MySettings:BaseUrl"]}/Api/Automation/CheckUpCall")
            })
                .Result;

            return Task.CompletedTask;
        }
    }
}
