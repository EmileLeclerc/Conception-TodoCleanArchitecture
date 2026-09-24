using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class CreateUseCase
{
    private readonly ITodoRepository _todoRepository;

    public CreateUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDto> Execute(CreateTodoDTO createTodoDTO)
    {
        Todo todoToCreate = new Todo(createTodoDTO.Title);
        Todo? todo = await _todoRepository.Add(todoToCreate);
        return new TodoDto(todo);
    }
}