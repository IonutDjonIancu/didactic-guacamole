using Microsoft.AspNetCore.Mvc;

namespace didactic_guacamole.Controllers;

[ApiController]
[Route("api")]
public class HealthController : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("Pong");
    }

}
