using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Shop.Domain.Infrastructure;
using WEB.Shop.Domain.Models;

namespace WEB.Shop.DataBase
{
    public class LogManager : ILogManager
    {
        private readonly ApplicationDbContext _context;

        public LogManager(ApplicationDbContext context) => _context = context;

        public async Task<int> CreateLogRecord(LogRecord record)
        {
            if (_context.LogRecords.Count() > 500)
            {
                await DeleteLog();
            }

            _context.LogRecords.Add(record);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteLog()
        {
            _context.LogRecords.Clear();
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<TResult> GetLog<TResult>(Func<LogRecord, TResult> selector) =>    
            _context.LogRecords
                .Select(selector)
                .ToList();
    }
}
