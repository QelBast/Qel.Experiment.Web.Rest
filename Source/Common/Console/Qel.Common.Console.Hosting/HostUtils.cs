using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Qel.Common.Console.Hosting;

# warning Подумать как бы вынести это в отдельную репу

public static class HostUtils
{
    public static IHost BuildConsoleHost(string[] args)
        => Host.CreateDefaultBuilder(args)
            .UseConsoleLifetime()
            //.ConfigureLogging()
            .ConfigureServices((context, services) 
                => services.BuildServiceProvider()).Build();

    public static IHost BuildDefaultConsoleApplicationHost(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddCustomDefaultConsoleServices();
        builder.Configuration.AddMyStandartConfigureProviders();
        //builder.Logging.AddMyLoggingBuilderConfigures<T>();
        //builder.Metrics.
        
        return builder.Build();
    }
}