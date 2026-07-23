using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.UseCase.Budget.Delete;
using PersonalFinance.Application.UseCase.Budget.GetAll;
using PersonalFinance.Application.UseCase.Budget.Register;
using PersonalFinance.Application.UseCase.Budget.Update;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Budget;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Communication.Responses.Budget;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
[Authorize]
public class BudgetController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(BudgetDto), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromServices] IRegisterBudgetUseCase useCase, [FromBody] RegisterBudgetRequest request)
    {
        BudgetDto response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpPut]
    [Route(template: "{budgetId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateBudgetUseCase useCase, [FromRoute] Guid budgetId, [FromBody] RegisterBudgetRequest request)
    {
        await useCase.Execute(budgetId: budgetId, request: request);
        return NoContent();
    }

    [HttpDelete]
    [Route(template: "{budgetId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteBudgetUseCase useCase, [FromRoute] Guid budgetId)
    {
        await useCase.Execute(budgetId: budgetId);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(GetAllBudgetResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllBudgetUseCase useCase)
    {
        GetAllBudgetResponse response = await useCase.Execute();
        return Ok(value: response);
    }
}
