using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagerApi.Test.Fixtures
{
    public class LoggerFixture<TEntity> : IDisposable
    {
        public ILogger<TEntity> Logger { get; private set; }

        public LoggerFixture()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            Logger = factory.CreateLogger<TEntity>();
        }

        public void Dispose()
        {
        }
    }
}
