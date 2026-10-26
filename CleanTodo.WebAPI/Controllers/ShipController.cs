
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ping")]
public class ShipController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("pong");
    }
}