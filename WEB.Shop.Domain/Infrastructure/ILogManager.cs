using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.Domain.Infrastructure
{
    public interface ILogManager
    {
        Task<int> CreateLogRecord(LogRecord record);
        Task<int> DeleteLog();
        IEnumerable<TResult> GetLog<TResult>(Func<LogRecord, TResult> selector);
    }

}
