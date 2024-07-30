using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDAL.LogProvider
{
    public class DatabaseLoggerProvider : ILoggerProvider
    {
      
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger();
        }

        public void Dispose()
        {
          
        }
    }
}
