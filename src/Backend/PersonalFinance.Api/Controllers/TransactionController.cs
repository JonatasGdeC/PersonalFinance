using Microsoft.AspNetCore.Mvc;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add()
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
    [Route(template: "category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(long categoryId)
    {
        return Ok();
    }

    [HttpGet]
    [Route(template: "{id}")]
    public async Task<IActionResult> GetById(long transactionId)
    {
        return Ok();
    }
}
