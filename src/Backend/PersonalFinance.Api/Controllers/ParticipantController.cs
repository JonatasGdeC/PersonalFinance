using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.UseCase.Participant.Delete;
using PersonalFinance.Application.UseCase.Participant.GetAll;
using PersonalFinance.Application.UseCase.Participant.GetById;
using PersonalFinance.Application.UseCase.Participant.Register;
using PersonalFinance.Application.UseCase.Participant.Update;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Participant;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Communication.Responses.Participant;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
[Authorize]
public class ParticipantController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(ParticipantDto), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterParticipantUseCase useCase, [FromBody] RegisterParticipantRequest request)
    {
        ParticipantDto response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpPut]
    [Route(template: "{participantId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateParticipantUseCase useCase, [FromRoute] Guid participantId, [FromBody] RegisterParticipantRequest request)
    {
        await useCase.Execute(participantId: participantId, request: request);
        return NoContent();
    }

    [HttpDelete]
    [Route(template: "{participantId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteParticipantUseCase useCase, [FromRoute] Guid participantId)
    {
        await useCase.Execute(participantId: participantId);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(GetAllParticipantResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllParticipantUseCase useCase)
    {
        GetAllParticipantResponse response = await useCase.Execute();
        return Ok(value: response);
    }

    [HttpGet]
    [Route(template: "{participantId}")]
    [ProducesResponseType(type: typeof(ParticipantDto), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetParticipantByIdUseCase useCase, [FromRoute] Guid participantId)
    {
        ParticipantDto response = await useCase.Execute(id: participantId);
        return Ok(value: response);
    }
}
