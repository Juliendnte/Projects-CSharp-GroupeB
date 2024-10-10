using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CalculatriceController : ControllerBase
{
    [HttpGet("addition")]
    public IActionResult Addition(double a, double b)
    {
        return Ok(a + b);
    }

    [HttpGet("soustraction")]
    public IActionResult Soustraction(double a, double b)
    {
        return Ok(a - b);
    }

    [HttpGet("multiplication")]
    public IActionResult Multiplication(double a, double b)
    {
        return Ok(a * b);
    }

    [HttpGet("division")]
    public IActionResult Division(double a, double b)
    {
        if (b == 0)
            return BadRequest("Division par zéro non autorisée.");
        return Ok(a / b);
    }
}
