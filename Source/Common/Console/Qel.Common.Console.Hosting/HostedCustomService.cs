using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Qel.Api.Transport;
using Qel.Api.Transport.Behaviours;
using Qel.Common.Console.Hosting.Models;

namespace Qel.Common.Console.Hosting;

public abstract class HostedCustomService(
    ILogger<HostedCustomService> logger,
    HealthCheckService health,
    IStartBehaviour? start,
    IProcessBehaviour? process,
    IFinishBehaviour? finish,
    IOptions<HostedOptions> options,
    IHostApplicationLifetime? lifetime = null) : BackgroundService 
{
    protected virtual ILogger<HostedCustomService> Logger { get; } = logger;
    protected virtual HostedOptions Options { get; } = options.Value;
    protected virtual HealthCheckService HealthService { get; } = health;
    protected virtual IHostApplicationLifetime? Lifetime { get; } = lifetime;
    protected virtual IStartBehaviour? StartBehaviour { get; } = start;
    protected virtual IProcessBehaviour? ProcessBehaviour { get; } = process;
    protected virtual IFinishBehaviour? FinishBehaviour { get; } = finish;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            do
            {
                await HealthService.CheckHealthAsync(stoppingToken);
                var startMessage = await StartIteration();
                if(Options.AllOnStartMethod)
                {
                    var processMessage = await ProcessIteration(startMessage);
                    await FinishIteration(processMessage);
                }
            }
            while (!stoppingToken.IsCancellationRequested);
        }
        finally
        {
            await StopAsync(stoppingToken);
        }
    }

    private async Task<BaseMessage?>? StartIteration()
    {
        using (Logger.BeginScope(nameof(StartBehaviour.GetType)))
        {
            try
            {
                if (Options.AllOnStartMethod)
                {
                    try
                    {
                        await StartBehaviour?.Start<BaseMessage>()!;
                    }
                    catch
                    {
                        Logger.LogWarning("Пусто");
                    }
                }
                else
                {

                }
                return await StartBehaviour?.Start<BaseMessage>()!;
            }
            catch (Exception ex)
            {
                Logger.LogCritical("StartIteration error! {mes}\n{stack}", ex.Message, ex.StackTrace);
            }
            return default;
        }
    }
    private async Task<BaseMessage?> ProcessIteration(BaseMessage? inMessage)
    {
        using (Logger.BeginScope(nameof(ProcessBehaviour.GetType)))
        {
            try
            {
                return await ProcessBehaviour?.Process(inMessage)!;
            }
            catch (Exception ex)
            {
                Logger.LogCritical("ProcessIteration error! {mes}\n", ex.Message);
            }
            return default;
        }
    }
    private async Task FinishIteration(BaseMessage? outMessage)
    {
        using (Logger.BeginScope(nameof(FinishBehaviour.GetType)))
        {
            try
            {
                await FinishBehaviour?.Finish(outMessage)!;
            }
            catch (Exception ex)
            {
                Logger.LogCritical("FinishIteration error! {mes}\n", ex.Message);
            }
        }
    }
}
