using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.Interfaces.Repositories;

public interface ITodoRepository
{
    Task<List<Todo>> GetAll();
    Task<Todo?> FindById(Guid id);
    Task<Todo?> Add(Todo todo);
    Task<Todo?> Delete(Guid id);

    Task<Todo?> ToggleCompleted(Guid id);

}
