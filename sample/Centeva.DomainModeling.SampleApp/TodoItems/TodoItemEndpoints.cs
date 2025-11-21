using Centeva.DomainModeling.SampleApp.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Centeva.DomainModeling.SampleApp.TodoItems;

/// <summary>
/// Minimal API endpoints for TodoItems
/// </summary>
public static class TodoItemEndpoints
{
    public static void MapTodoItemEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/todoitems")
            .WithName("TodoItems");

        group.MapGet("{id:int}", GetTodoItem)
            .WithName("GetTodoItem")
            .WithDescription("Get a todo item by id");

        group.MapPost("/", CreateTodoItem)
            .WithName("CreateTodoItem")
            .WithDescription("Create a new todo item");
    }

    private static async Task<Results<Ok<TodoItemDto>, NotFound>> GetTodoItem(int id, ApplicationDbContext dbContext)
    {
        var todo = await dbContext.TodoItems.FindAsync(id);

        return todo is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new TodoItemDto(todo.Id, todo.Name, todo.Description));
    }

    private static async Task<IResult> CreateTodoItem(CreateTodoCommand command, ApplicationDbContext dbContext)
    {
        var todo = new TodoItem(command.Name)
        {
            Description = command.Description
        };

        dbContext.TodoItems.Add(todo);

        await dbContext.SaveChangesAsync();

        return TypedResults.Created($"/todoitems/{todo.Id}", (object?)null);
    }
}
