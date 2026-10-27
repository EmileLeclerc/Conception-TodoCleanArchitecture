using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class CreateTodoUseCase
{
    private readonly ITodoRepository _todoRepository;

    public CreateTodoUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<TodoDTO> Execute(CreateTodoDTO createTodoDTO)
    {
        Todo todoToCreate = new Todo(createTodoDTO.Title);
        Todo? todo = await _todoRepository.Add(todoToCreate);
        return new TodoDTO(todo);
    }
}