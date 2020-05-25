using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WEB.Shop.UI.Automation
{
    class JobWakeUpCall : IJob
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;

        public JobWakeUpCall(IHttpClientFactory httpFactory, IConfiguration configuration)
        {
            _httpFactory = httpFactory;
            _configuration = configuration;
        }

        public Task Execute(IJobExecutionContext context)
        {
            using var client = _httpFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod("GET"),
                RequestUri = new Uri($"{_configuration["MySettings:BaseUrl"]}/Api/Automation/WakeUpCall")
            };

            var response = client.SendAsync(httpRequestMessage).Result;

            return Task.CompletedTask;
        }
    }
}
