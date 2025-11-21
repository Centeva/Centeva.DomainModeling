namespace Centeva.DomainModeling.SampleApp.TodoItems;

public class CreateTodoCommand
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
