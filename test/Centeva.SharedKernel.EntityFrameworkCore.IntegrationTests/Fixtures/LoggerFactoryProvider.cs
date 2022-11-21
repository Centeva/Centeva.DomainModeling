using Microsoft.Extensions.Logging;

namespace Centeva.SharedKernel.EntityFrameworkCore.IntegrationTests.Fixtures;

public class LoggerFactoryProvider
{
    public static readonly ILoggerFactory LoggerFactoryInstance = LoggerFactory.Create(builder =>
    {
        builder.AddFilter("Centeva.SharedKernel", LogLevel.Debug);
        builder.AddConsole();
    });
}