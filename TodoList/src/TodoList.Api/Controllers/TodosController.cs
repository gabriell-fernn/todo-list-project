using Microsoft.AspNetCore.Mvc;
using TodoList.Application.UseCases.Todos.GetAll;
using TodoList.Application.UseCases.Todos.GetById;
using TodoList.Application.UseCases.Todos.Register;
using TodoList.Application.UseCases.Todos.Sync;
using TodoList.Application.UseCases.Todos.Update;
using TodoList.Communication.Requests;
using TodoList.Exception.ExceptionBase;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(
            [FromBody] RequestRegisterTodoJson request,
            [FromServices] IRegisterTodoUseCase useCase)
        {
            var response = await useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromServices] IGetAllTodosUseCase useCase,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string order = "asc",
            [FromQuery] string? title = null,
            [FromQuery] string sort = "id"
            )
        {
            try
            {
                var request = new RequestGetTodosJson
                {
                    Page = page,
                    PageSize = pageSize,
                    Title = title,
                    Sort = sort,
                    Order = order
                };

                var response = await useCase.Execute(request);
                return Ok(response);
            }
            catch (ErrorOnValidationException ex)
            {
                return BadRequest(new { errors = ex });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            [FromRoute] int id,
            [FromServices] IGetTodoByIdUseCase useCase)
        {
            var response = await useCase.Execute(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] RequestUpdateTodoJson request,
            [FromServices] IUpdateTodoUseCase useCase)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }

        [HttpPost("sync")]
        public async Task<IActionResult> Sync(
            [FromServices] ISyncTodosUseCase useCase)
        {
            var request = new RequestSyncTodosJson();
            var response = await useCase.Execute(request);
            return Ok(response);
        }
    }
}
