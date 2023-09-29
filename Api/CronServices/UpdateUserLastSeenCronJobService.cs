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
            var values = CacheHelper.Get<Dictionary<string, DateTime>>(cacheKey) ?? new Dictionary<string, DateTime>();
            if (!values.Any()) return;
            await userService.BulkUpdateLastSeenAsync(values);
            await CacheHelper.SetAsync(cacheKey, new Dictionary<string, DateTime>(), TimeSpan.FromDays(1));
        }
    }
}
