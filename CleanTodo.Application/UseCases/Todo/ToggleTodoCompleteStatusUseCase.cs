using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class ToggleTodoCompleteStatusUseCase
{
    private readonly ITodoRepository _todoRepository;

    public ToggleTodoCompleteStatusUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDto> Execute(Guid id)
    {
        Todo? todo = await _todoRepository.ToggleCompleted(id);
        if (todo == null)
        {
            throw new NotFoundException(id);
        }
        return new TodoDto(todo);
    }
}