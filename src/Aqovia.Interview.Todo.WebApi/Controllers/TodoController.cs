using Aqovia.Interview.Http.DTO;
using Aqovia.Interview.Todo.Data.DTO;
using Aqovia.Interview.Todo.Data.Extensions;
using Aqovia.Interview.Todo.Data.Models;
using Aqovia.Interview.Todo.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aqovia.Interview.Todo.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly TodoService _todoService;

    public TodoController(
        ILogger<TodoController> logger,
        TodoService todoService
    ) {
        this._logger = logger;
        this._todoService = todoService;
    }

    [HttpGet(Name = nameof(GetTodos))]
    public async Task<ActionResult<TodoDto[]>> GetTodos() {
        var allTodos = this._todoService.GetTodos(x => true);
        return new OkObjectResult(allTodos.Select(tdm => tdm.ToTodoDto()).ToArray());
    }

    [HttpGet("{id}", Name = nameof(GetTodo))]
    public async Task<ActionResult<TodoDto>> GetTodo(Guid id) {
        return this._todoService.TryGetTodoById(id, out TodoModel? tdm) ? new NotFoundResult() : new OkObjectResult(tdm);
    }

    [HttpPost(Name = nameof(CreateTodo))]
    public async Task<IActionResult> CreateTodo([FromBody] TodoInputDto input) {
        var newTodo = this._todoService.AddTodo(input.Completed, input.Description);
        return new CreatedAtRouteResult(nameof(GetTodo), new { id = newTodo.Id }, newTodo);
    }

    [HttpPut("{id}", Name = nameof(UpdateTodo))]
    public async Task<IActionResult> UpdateTodo(Guid id, [FromBody] TodoInputDto input) {
        try {            
            this._todoService.UpdateTodo(id, input.Completed, input.Description);
        }
        catch (KeyNotFoundException knfe) {
            this._logger.LogError(knfe, knfe.Message);
            return new BadRequestObjectResult(new ErrorDto() {
                Message = "Todo not found"
            });
        }
        return new NoContentResult();
    }

    [HttpDelete("{id}", Name = nameof(DeleteTodo))]
    public async Task<IActionResult> DeleteTodo(Guid id) {
        this._todoService.DeleteTodo(id);
        return new OkResult();
    }
}
