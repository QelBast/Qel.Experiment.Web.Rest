using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Qel.Ef.Providers.Common;

public class ProviderSelector
{
    public ProviderSelector(IEnumerable<IProviderConfigurator> configurators)
    {
        Configurators = configurators;
    }

    public IEnumerable<IProviderConfigurator> Configurators { get; }

    public DbContextOptionsBuilder SelectProvider(string key, DbContextOptionsBuilder builder, IConfiguration config)
    {
        var providerName = config.GetRequiredSection(key).GetRequiredSection("DbProvider").Value;

        foreach (var configurator in Configurators)
        {
            if (configurator.Tag == providerName)
            {
                return configurator.ConfigureOptionsBuilder(builder, config);
            }
        }
        return builder;
    }
}
