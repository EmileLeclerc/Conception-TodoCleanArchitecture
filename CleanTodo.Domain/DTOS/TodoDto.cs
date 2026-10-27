using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS;

public class TodoDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime Date { get; set; }

    public TodoDTO() { }


    // Devrait être fait dans Mapping -> automapper.
    public TodoDTO(Todo todo)
    {

        Id = todo.Id;
        Title = todo.Text;
        Date = todo.Date;
        IsCompleted = todo.IsCompleted;
    }
}
