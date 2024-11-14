using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Qel.Common.Console.Hosting;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDefaultConsoleServices(this IServiceCollection services, params ILoggingBuilder[] loggings)
    {

        foreach(var logging in loggings)
        {
            services
                .AddLogging(x => x = logging);
        }

        services.AddCustomDefaultHealthChecks();
        return services;
    }

    public static IServiceCollection AddCustomDefaultHealthChecks(this IServiceCollection services)
    {
        services
            .AddResourceMonitoring()
            .AddHealthChecks()
                .AddResourceUtilizationHealthCheck()
                .AddApplicationLifecycleHealthCheck();
        
        return services;
    }
}
