using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Application.Logs
{
    [TransientService]
    public class CreateLogRecord
    {
        private readonly ILogManager _logManager;

        public CreateLogRecord(ILogManager logManager) => _logManager = logManager;

        public async Task<int> DoAsync(Request request)
        {
            var record = new LogRecord
            {
                IP = request.IP,
                Message = request.Message,
                TimeStamp = request.TimeStamp
            };

            if (await _logManager.CreateLogRecord(record) <= 0)
            {
                throw new Exception("Failed to add record!");
            }

            return record.Id;
        }

        public class Request
        {
            public DateTime TimeStamp { get; set; }
            public string Message { get; set; }
            public string IP { get; set; }
        }
    }
}
