using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.UseCase.Bill.Delete;
using PersonalFinance.Application.UseCase.Bill.GetAll;
using PersonalFinance.Application.UseCase.Bill.GetDashboard;
using PersonalFinance.Application.UseCase.Bill.Register;
using PersonalFinance.Application.UseCase.Bill.Update;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Bill;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Communication.Responses.Bill;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
[Authorize]
public class BillController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(BillDto), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromServices] IRegisterBillUseCase useCase, [FromBody] RegisterBillRequest request)
    {
        BillDto response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpPut]
    [Route(template: "{billId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateBillUseCase useCase, [FromRoute] Guid billId, [FromBody] RegisterBillRequest request)
    {
        await useCase.Execute(billId: billId, request: request);
        return NoContent();
    }

    [HttpDelete]
    [Route(template: "{billId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteBillUseCase useCase, [FromRoute] Guid billId)
    {
        await useCase.Execute(billId: billId);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(GetAllBillResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllBillUseCase useCase, [FromQuery] BillFilterRequest filter)
    {
        GetAllBillResponse response = await useCase.Execute(filter: filter);
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "dashboard")]
    [ProducesResponseType(type: typeof(GetBillDashboardResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboard([FromServices] IGetBillDashboardUseCase useCase)
    {
        GetBillDashboardResponse response = await useCase.Execute();
        return Ok(value: response);
    }
}
