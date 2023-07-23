using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PreOrderPlatform.API.HealthChecks
{
    public class SystemResourcesHealthCheck : IHealthCheck
    {
        private readonly IMemoryCache _cache;



        public SystemResourcesHealthCheck(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken()
        )
        {
            var memoryUsageBytes = GC.GetTotalMemory(false);
            var memoryUsageMB = memoryUsageBytes / (1024 * 1024); // Convert to MB
            var totalMemoryBytes = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
            var totalMemoryMB = totalMemoryBytes / (1024 * 1024); // Convert to MB
            var threadCount = Process.GetCurrentProcess().Threads.Count;
            var requestCount = _cache.Get<int>("RequestCount");
            var responseCount = _cache.Get<int>("ResponseCount");

            var memoryHealth = memoryUsageMB < 1024; // Threshold is 1GB
            var threadHealth = threadCount < 200; // Threshold is 200 threads


            var requestTimestamps = _cache.Get<List<DateTime>>("RequestTimestamps");
            var responseTimestamps = _cache.Get<List<DateTime>>("ResponseTimestamps");

            var requestCountLastHour = requestTimestamps?.Count(t => t > DateTime.UtcNow.AddHours(-1)) ?? 0;
            var requestCountLast24Hours = requestTimestamps?.Count(t => t > DateTime.UtcNow.AddDays(-1)) ?? 0;

            var responseCountLastHour = responseTimestamps?.Count(t => t > DateTime.UtcNow.AddHours(-1)) ?? 0;
            var responseCountLast24Hours = responseTimestamps?.Count(t => t > DateTime.UtcNow.AddDays(-1)) ?? 0;


            var data = new Dictionary<string, object>()
            {
                { "MemoryUsageMB", memoryUsageMB },
                { "TotalMemoryMB", totalMemoryMB },
                { "ThreadCount", threadCount },
                { "RequestCount", requestCount },
                { "ResponseCount", responseCount }
            };

            if (memoryHealth && threadHealth)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("System resources are healthy.", data)
                );
            }
            else
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy(
                        "System resources are unhealthy.",
                        exception: null,
                        data
                    )
                );
            }
        }


        private static void IncrementCounter(IMemoryCache cache, string key)
        {
            var count = cache.Get<int>(key);
            cache.Set(key, count + 1);
        }

        private static void AddTimestamp(IMemoryCache cache, string key, DateTime timestamp)
        {
            var list = cache.Get<List<DateTime>>(key) ?? new List<DateTime>();
            list.Add(timestamp);
            cache.Set(key, list);
        }


    }



}