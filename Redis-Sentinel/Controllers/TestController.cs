using Microsoft.AspNetCore.Mvc;
using Redis_Sentinel.Services;

namespace Redis_Sentinel.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("[Action]/{key}/{Value}")]
    public async Task<IActionResult> SetValue(string key, string value)
    {
        var redis = await RedisService.RedisMasterDatabase();
        await redis.StringSetAsync(key, value);
        return Ok();
    }

    [HttpGet("[Action]/{key}")]
    public async Task<IActionResult> GetValue(string key)
    {
        var redis = await RedisService.RedisMasterDatabase();
        var data = await redis.StringGetAsync(key);
        return Ok(data.ToString());
    }
}