using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS;

public class CreateTodoDTO
{
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

    public CreateTodoDTO() { }


    public CreateTodoDTO(Todo todo)
    {
        Title = todo.Text;
        IsCompleted = todo.IsCompleted;
    }
}
