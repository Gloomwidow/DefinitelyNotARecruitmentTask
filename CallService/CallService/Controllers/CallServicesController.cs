using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace CallService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallServicesController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;

        private const string RestResponseCacheKey = "REST";
        private const string RestServiceAddress = "https://localhost:7202/somedata";

        private const int CacheTimeMinutes = 1;

        public CallServicesController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetRestServiceResponse()
        {
            if (!this.memoryCache.TryGetValue(RestResponseCacheKey, out var response)) 
            {
                response = await GetRestDataFromAPI();

                var cachePolicy = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(CacheTimeMinutes));

                this.memoryCache.Set(RestResponseCacheKey, response, cachePolicy);
            }

            return Ok(response);
        }


        private async Task<string> GetRestDataFromAPI()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(RestServiceAddress);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }

            return "Error";
        }
    }
}
