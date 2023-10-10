using Business.Abstraction.Services;
using RedisCacheHelper;
using Scheduler;

namespace Api.CronServices
{
    public class UpdateUserLastSeenCronJob : ScheduledJob
    {
        public override DateTime GetNextExecutionSchedule()
        {
            var prev = PreviousExecutionDateTime ?? DateTime.UtcNow;
            return prev.AddMinutes(10);
        }

        public override async Task Execute(IServiceProvider serviceProvider)
        {
            const string cacheKey = "UsersLastSeen";
            var userService = serviceProvider.GetRequiredService<IUserService>();
            var values = await CacheHelper.GetAsync<Dictionary<string, DateTime>>(cacheKey).ConfigureAwait(false) ?? new Dictionary<string, DateTime>();
            if (values.Count == 0) return;
            await userService.BulkUpdateLastSeenAsync(values).ConfigureAwait(false);
            await CacheHelper.SetAsync(cacheKey, new Dictionary<string, DateTime>(), TimeSpan.FromDays(1)).ConfigureAwait(false);
        }
    }
}
