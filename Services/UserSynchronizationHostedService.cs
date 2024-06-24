using System;
using System.Threading;
using System.Threading.Tasks;
using CRM.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class UserSynchronizationHostedService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public UserSynchronizationHostedService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Set the timer to execute the DoWork method immediately and then every 10 minutes
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        Console.WriteLine("users periodic cycle done");
        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var synchronizationService = scope.ServiceProvider.GetRequiredService<UserSynchronizationService>();
            synchronizationService.SynchronizeUsersAsync().GetAwaiter().GetResult();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
