
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/ship")]
public class ShipController : ControllerBase
{

    [HttpPost("dock")]
    public IActionResult Dock()
    {
        return Ok(new
        {
            message = "ship docked successfully"
        });
    }

    [HttpGet("/api/ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}