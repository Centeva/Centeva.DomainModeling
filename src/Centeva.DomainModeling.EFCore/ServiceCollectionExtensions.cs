using Centeva.DomainModeling.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Centeva.DomainModeling.EFCore;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add the EF Core implementation of IUnitOfWork to the DI container
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWork, UnitOfWork>();
}