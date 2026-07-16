using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.UseCase.Transaction.Delete;
using PersonalFinance.Application.UseCase.Transaction.GetAll;
using PersonalFinance.Application.UseCase.Transaction.GetByCategory;
using PersonalFinance.Application.UseCase.Transaction.GetDashboard;
using PersonalFinance.Application.UseCase.Transaction.Register;
using PersonalFinance.Application.UseCase.Transaction.Update;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests;
using PersonalFinance.Communication.Requests.Transaction;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Communication.Responses.Transaction;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
[Authorize]
public class TransactionController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(TransactionDto), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromServices] IRegisterTransactionUseCase useCase, [FromBody] RegisterTransactionRequest request)
    {
        TransactionDto response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpPut]
    [Route(template: "{transactionId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateTransactionUseCase useCase, [FromRoute] long transactionId, [FromBody] RegisterTransactionRequest request)
    {
        await useCase.Execute(transaction: transactionId, request: request);
        return NoContent();
    }

    [HttpDelete]
    [Route(template: "{transactionId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteTransactionUseCase useCase, [FromRoute] long transactionId)
    {
        await useCase.Execute(transactionId: transactionId);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(GetListTransactionsResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllTransactionUseCase useCase, [FromQuery] TransactionFilterRequest request)
    {
        GetListTransactionsResponse response = await useCase.Execute(request: request);
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "dashboard")]
    [ProducesResponseType(type: typeof(GetTransactionDashboardResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboard([FromServices] IGetTransactionDashboardUseCase useCase, [FromQuery] DateTime date)
    {
        GetTransactionDashboardResponse response = await useCase.Execute(date: date);
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "category/{categoryId}")]
    [ProducesResponseType(type: typeof(GetListTransactionsResponse), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByCategory([FromServices] IGetTransactionByCategoryIdUseCase useCase, [FromRoute] long categoryId, [FromQuery] DateTime date, [FromQuery] PaginationRequest pagination)
    {
        GetListTransactionsResponse response = await useCase.Execute(categoryId: categoryId, date: date, pagination: pagination);
        return Ok(value: response);
    }
}
