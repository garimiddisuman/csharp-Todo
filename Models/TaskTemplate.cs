namespace MyTodo;

public class TaskTemplate
{
    public int? Id { get; set; }
    public required string Title { get; set; }
    public required bool IsDone { get; set; }
}