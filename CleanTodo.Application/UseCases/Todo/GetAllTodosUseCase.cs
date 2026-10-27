using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class GetAllTodosUseCase
{
    private readonly ITodoRepository _todoRepository;

    public GetAllTodosUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IList<TodoDTO>> Execute()
    {
        var todos = await _todoRepository.GetAll();
        return todos.Select(x => new TodoDTO(x)).ToList();
    }
}