using System.Net.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Qel.Api.Transport.Behaviours;
using Qel.Common.Console.Hosting.RabbitMq;
using Qel.Ef.Contexts.MainContext;
using Qel.Ef.DbClient;
using Qel.Ef.DbClient.Extensions;
using Qel.Ef.Providers.Postgres;
using Qel.Experiments.Web.Rest.RequestProvider;
using Qel.Experiments.Web.Rest.RequestProvider.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = Host
            .CreateApplicationBuilder(args)
            .PackRabbitMqConsoleApplicationHost();
        host.Services
            .AddTransient<IProcessBehaviour, RequestProcessor>()
            .AddTransient<VerificationService>()
            .Configure<VerificationOptions>(host.Configuration.GetSection(nameof(VerificationOptions)))
            .AddHttpClient()
            .ConfigureHttpClientDefaults(df => df.ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                    {
                        return sslPolicyErrors == SslPolicyErrors.None ||
                                sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch;
                    }
                };
            }
            ));
        
        host.Services.AddDbClient<DbContextMain>(register =>
            {
                register.AddTransientRepository<IRequestRepository, RequestRepository<DbContextMain>>();
            },
            host.Configuration, 
            [new Configurator(nameof(DbContextMain))]);
            
        var app = host.Build();
        app.Run();
    }
}