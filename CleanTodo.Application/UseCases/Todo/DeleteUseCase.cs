using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class DeleteUseCase
{
    private readonly ITodoRepository _todoRepository;

    public DeleteUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDto> Execute(Guid id)
    {
        Todo? todo = await _todoRepository.Delete(id);
        if (todo == null)
        {
            throw new NotFoundException(id);
        }
        return new TodoDto(todo);
    }
}