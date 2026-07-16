using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.UseCase.Pot.Delete;
using PersonalFinance.Application.UseCase.Pot.GetAll;
using PersonalFinance.Application.UseCase.Pot.Register;
using PersonalFinance.Application.UseCase.Pot.Update;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Pot;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Communication.Responses.Pot;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
[Authorize]
public class PotController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(PotDto), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterPotUseCase useCase, [FromBody] RegisterPotRequest request)
    {
        PotDto response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpPut]
    [Route(template: "{potId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdatePotUseCase useCase, [FromRoute] long potId, [FromBody] RegisterPotRequest request)
    {
        await useCase.Execute(potId: potId, request: request);
        return NoContent();
    }

    [HttpDelete]
    [Route(template: "{potId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeletePotUseCase useCase, [FromRoute] long potId)
    {
        await useCase.Execute(potId: potId);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(GetAllPotsResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllPotUseCase useCase)
    {
        GetAllPotsResponse response = await useCase.Execute();
        return Ok(value: response);
    }
}
