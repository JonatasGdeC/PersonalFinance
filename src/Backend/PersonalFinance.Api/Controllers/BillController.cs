using Microsoft.AspNetCore.Mvc;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
public class BillController : ControllerBase
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

    [HttpGet]
    [Route(template: "dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        return Ok();
    }

    [HttpGet]
    [Route(template: "{id}")]
    public async Task<IActionResult> GetById(long billId)
    {
        return Ok();
    }
}
