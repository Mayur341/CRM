using System.Threading;
using System.Threading.Tasks;
using CRM.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

public class CustomSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IServiceScopeFactory _scopeFactory;

    public CustomSaveChangesInterceptor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        TriggerSynchronization();
        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await TriggerSynchronizationAsync();
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private void TriggerSynchronization()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var synchronizationService = scope.ServiceProvider.GetRequiredService<UserSynchronizationService>();
            synchronizationService.SynchronizeUsersAsync().GetAwaiter().GetResult();
        }
    }

    private async Task TriggerSynchronizationAsync()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var synchronizationService = scope.ServiceProvider.GetRequiredService<UserSynchronizationService>();
            await synchronizationService.SynchronizeUsersAsync();
        }
    }
}
