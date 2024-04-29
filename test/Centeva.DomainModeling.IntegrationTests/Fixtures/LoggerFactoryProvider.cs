using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Centeva.DomainModeling.IntegrationTests.Fixtures;

public class LoggerFactoryProvider
{
    public static readonly ILoggerFactory LoggerFactoryInstance = LoggerFactory.Create(builder =>
    {
        builder.AddFilter("Centeva.DomainModeling", LogLevel.Debug);
        builder.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Debug);
        builder.AddConsole();
    });
}