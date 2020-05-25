using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;

namespace WEB.Shop.Application.Logs
{
    [TransientService]
    public class DeleteLog
    {
        private readonly ILogManager _logManager;

        public DeleteLog(ILogManager logManager) => _logManager = logManager;

        public async Task<int> ClearDataBaseAsync() => await _logManager.DeleteLog();
    }
}
