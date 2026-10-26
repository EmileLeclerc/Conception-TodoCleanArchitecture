
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/ship")]
[ApiController]
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

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}