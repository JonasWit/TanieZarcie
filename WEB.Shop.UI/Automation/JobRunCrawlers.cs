using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WEB.Shop.UI.Automation
{
    class JobRunCrawlers : IJob
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _configuration;

        public JobRunCrawlers(IHttpClientFactory httpFactory, IConfiguration configuration)
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
                RequestUri = new Uri($"{_configuration["MySettings:BaseUrlProd"]}/Api/Automation/RunCrawlers")
            };

            httpRequestMessage.Headers.Add("ApiKey", "AdminTZApiAccess");

            var response = client.SendAsync(httpRequestMessage).Result;

            return Task.CompletedTask;
        }
    }
}
