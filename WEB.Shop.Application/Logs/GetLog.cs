using System;
using System.Collections.Generic;
using System.Text;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Logs
{
    [TransientService]
    public class GetLog
    {
        private readonly ILogManager _logManager;

        public GetLog(ILogManager logManager) => _logManager = logManager;

        public IEnumerable<LogRecordVirewModel> GetAllProducts() =>
            _logManager.GetLog(x => new LogRecordVirewModel
            {
                Id = x.Id,
                IP = x.IP,
                Message = x.Message,
                TimeStamp = x.TimeStamp
            });

        public class LogRecordVirewModel
        {
            public int Id { get; set; }
            public DateTime TimeStamp { get; set; }
            public string Message { get; set; }
            public string IP { get; set; }
        }
    }
}
