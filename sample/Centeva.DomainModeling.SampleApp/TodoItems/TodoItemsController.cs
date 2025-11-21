using Centeva.DomainModeling.SampleApp.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Centeva.DomainModeling.SampleApp.TodoItems;

[ApiController]
[Route("[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public TodoItemsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var todo = await _dbContext.TodoItems.FindAsync(id);

        return todo is null ? NotFound() : Ok(new TodoItemDto(todo.Id, todo.Name, todo.Description));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTodoCommand command)
    {
        var todo = new TodoItem(command.Name)
        {
            Description = command.Description
        };

        _dbContext.TodoItems.Add(todo);

        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = todo.Id }, null);
    }
}
