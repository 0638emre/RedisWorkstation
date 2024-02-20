using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers;

[ApiController]
[Route("[controller]")]
public class ValuesController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;

    public ValuesController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    [HttpGet("SetName/{name}")]
    public void Set(string name)
    {
        _memoryCache.Set("name", name);
    }
    
    [HttpGet("GetName")]
    public string Get()
    {
         return _memoryCache.Get<string>("name");
         // return _memoryCache.Get("name"); //Böyle object döndürür

    }

    [HttpGet("TryGeyValueName")]
    public string TryGetValue()
    {
        if (_memoryCache.TryGetValue<string>("name", out string name))//kontrollü bir cache get işlemi.
        {
            return name;
        }
        return "null";
    }

    [HttpGet("SetDate")]
    public void SetDate()
    {
        _memoryCache.Set<DateTime>("date", DateTime.Now, options: new MemoryCacheEntryOptions()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(5)
        });
    }

    [HttpGet("GetDate")]
    public DateTime GetDate()
    {
        return _memoryCache.Get<DateTime>("date");
    }
}