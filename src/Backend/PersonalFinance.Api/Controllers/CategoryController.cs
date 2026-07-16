using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.Application.UseCase.Category.Delete;
using PersonalFinance.Application.UseCase.Category.GetAll;
using PersonalFinance.Application.UseCase.Category.Register;
using PersonalFinance.Application.UseCase.Category.Update;
using PersonalFinance.Communication.Dtos;
using PersonalFinance.Communication.Requests.Category;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Communication.Responses.Category;

namespace PersonalFinance.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(CategoryDto), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterCategoryUseCase useCase, [FromBody] RegisterCategoryRequest request)
    {
        CategoryDto response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpPut]
    [Route(template: "{categoryId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateCategoryUseCase useCase, [FromRoute] long categoryId, [FromBody] RegisterCategoryRequest request)
    {
        await useCase.Execute(categoryId: categoryId, request: request);
        return NoContent();
    }

    [HttpDelete]
    [Route(template: "{categoryId}")]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteCategoryUseCase useCase, [FromRoute] long categoryId)
    {
        await useCase.Execute(categoryId: categoryId);
        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(type: typeof(GetAllCategoryResponse), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllCategoryUseCase useCase)
    {
        GetAllCategoryResponse response = await useCase.Execute();
        return Ok(value: response);
    }
}
