using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class DeleteTodoUseCase
{
    private readonly ITodoRepository _todoRepository;

    public DeleteTodoUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDTO> Execute(Guid id)
    {
        Todo? todo = await _todoRepository.Delete(id);
        if (todo == null)
        {
            throw new NotFoundException(id);
        }
        return new TodoDTO(todo);
    }
}