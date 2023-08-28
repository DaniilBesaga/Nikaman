using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Nikaman.Middlewares
{
    public class IPFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public IPFilterMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }
        public async Task Invoke(HttpContext context)
        {
            string ip = context.Connection.RemoteIpAddress.ToString();
            var cachedData = _cache.Get(ip);
            if (cachedData == null)
            {
                // Если данные не найдены в кэше, сохраняем данные в кэше
                var data = "1";
                int a = 1;
                _cache.Set(ip, a, TimeSpan.FromMinutes(1));
            }
            await _next(context);
        }
    }
}
