using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private const int RequestLimit = 2000;
        private const int TimeWindowMinutes = 1;
        private static ConcurrentDictionary<string, List<DateTime>> _ipRequestTimestamps = new();

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMemoryCache cache)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();

            if (IsIpBlocked(ipAddress))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return;
            }

            IncrementIpRequest(ipAddress);

            await _next(context);
        }

        private bool IsIpBlocked(string ipAddress)
        {
            if (!_ipRequestTimestamps.TryGetValue(ipAddress, out var timestamps))
            {
                return false;
            }

            timestamps.RemoveAll(x => x < DateTime.UtcNow.AddMinutes(-TimeWindowMinutes));

            return timestamps.Count > RequestLimit;
        }

        private void IncrementIpRequest(string ipAddress)
        {
            var timestamps = _ipRequestTimestamps.GetOrAdd(ipAddress, _ => new List<DateTime>());

            lock (timestamps)
            {
                timestamps.Add(DateTime.UtcNow);
            }
        }
    }
}
