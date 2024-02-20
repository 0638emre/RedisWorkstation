using Microsoft.AspNetCore.Mvc;

namespace Redis_Sentinel.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("[Action]/{key}/{Value}")]
    public IActionResult SetValue(string key, string value)
    {
        return Ok();
    }
    
    [HttpGet("[Action]/{key}")]
    public IActionResult GetValue(string key)
    {
        return Ok();
    }
}