using System.Text;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCaching.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IDistributedCache _distributedCache;

    public TestController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    [HttpGet("set")]
    public  async Task<IActionResult> Set(string name, string surname)
    {
        await _distributedCache.SetStringAsync("name", name, options: new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(5)
        });
        
        await _distributedCache.SetAsync("surname", Encoding.UTF8.GetBytes(surname), options: new()
        {
            AbsoluteExpiration = DateTime.Now.AddSeconds(30),
            SlidingExpiration = TimeSpan.FromSeconds(5)
        });

        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
       var name  = await _distributedCache.GetStringAsync("name");
       var surname = await _distributedCache.GetAsync("surname");

       var sn = Encoding.UTF8.GetString(surname);

       return Ok(new
       {
            name = name,
            surname = sn
       });
    }
}