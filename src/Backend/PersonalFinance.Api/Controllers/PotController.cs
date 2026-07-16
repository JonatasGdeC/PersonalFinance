using Microsoft.AspNetCore.Mvc;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
public class PotController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update()
    {
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }
}