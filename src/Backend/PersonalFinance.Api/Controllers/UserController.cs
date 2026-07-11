using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.UseCase.User.Register;
using PersonalFinance.Communication.Requests.User;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Communication.Responses.User;

namespace PersonalFinance.Api.Controllers;

[Microsoft.AspNetCore.Components.Route(template: "[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(type: typeof(RegisterUserResponse), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RegisterUserRequest request)
    {
        RegisterUserResponse response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    // [HttpPost]
    // [Microsoft.AspNetCore.Components.Route(template: "login")]
    // [EnableRateLimiting(policyName: "login")]
    // [AllowAnonymous]
    // [ProducesResponseType(type: typeof(LoginResponse), statusCode: StatusCodes.Status200OK)]
    // [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status401Unauthorized)]
    // public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] LoginRequest request)
    // {
    //     LoginResponse response = await useCase.Execute(request: request);
    //     return Ok(value: response);
    // }
    //
    // [HttpPost]
    // [Microsoft.AspNetCore.Components.Route(template: "forgot-password")]
    // [EnableRateLimiting(policyName: "forgot-password")]
    // [AllowAnonymous]
    // [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    // public async Task<IActionResult> ForgotPassword([FromServices] IForgotPassword useCase, [FromBody] ForgotPasswordRequest request)
    // {
    //     await useCase.Execute(request: request);
    //     return NoContent();
    // }
    //
    // [HttpPost]
    // [Microsoft.AspNetCore.Components.Route(template: "validate-reset-code")]
    // [AllowAnonymous]
    // [ProducesResponseType(type: typeof(ValidateResetCodeResponse), statusCode: StatusCodes.Status200OK)]
    // [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> ValidateResetCode([FromServices] IValidateResetCodeUseCase useCase, [FromBody] ValidateResetCodeRequest request)
    // {
    //     ValidateResetCodeResponse response = await useCase.Execute(request: request);
    //     return Ok(value: response);
    // }
    //
    // [HttpPut]
    // [Microsoft.AspNetCore.Components.Route(template: "reset-password")]
    // [AllowAnonymous]
    // [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    // [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> ResetPassword([FromServices] IResetPassword useCase, [FromBody] ResetPasswordRequest request)
    // {
    //     await useCase.Execute(request: request);
    //     return NoContent();
    // }
    //
    // [HttpPut]
    // [Authorize]
    // [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    // [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> Update([FromServices] IUpdateUserUseCase useCase, [FromBody] UpdateUserRequest request)
    // {
    //     await useCase.Execute(request: request);
    //     return NoContent();
    // }
    //
    // [HttpPut(template: "password")]
    // [Authorize]
    // [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    // [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> UpdatePassword([FromServices] IUpdatePasswordUseCase useCase, [FromBody] UpdatePasswordRequest request)
    // {
    //     await useCase.Execute(request: request);
    //     return NoContent();
    // }
    //
    // [HttpGet]
    // [Authorize]
    // [ProducesResponseType(type: typeof(UserDto), statusCode: StatusCodes.Status200OK)]
    // public async Task<IActionResult> Get([FromServices] IGetUserUseCase useCase)
    // {
    //     UserDto user = await useCase.Execute();
    //     return Ok(value: user);
    // }
    //
    // [HttpDelete]
    // [Authorize]
    // [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    // public async Task<IActionResult> Delete([FromServices] IDeleteUserUseCase useCase)
    // {
    //     await useCase.Execute();
    //     return NoContent();
    // }
}