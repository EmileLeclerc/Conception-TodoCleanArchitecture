using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TodoController(GetAllTodosUseCase getAllUseCase, GetTodoUseCase getTodoUseCase, CreateUseCase createUseCase, DeleteUseCase deleteUseCase, ToggleTodoCompleteStatusUseCase toggleTodoCompleteStatusUseCase) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
    {
        var todos = await getAllUseCase.Execute();
        return Ok(todos);
    }

    //Cadeau! pour le create.On utilise un CreatedAtAction qui retourne un code http 201 et un header location avec l'url du nouvel élément créé.


    [HttpPost]
    public async Task<ActionResult<TodoDto>> Create([FromBody] CreateTodoDTO createTodoDto)
    {
        TodoDto todo = await createUseCase.Execute(createTodoDto);

        return CreatedAtAction(
            nameof(Get),
            new { id = todo.Id },
            todo);
    }

    [HttpGet("{id}")] // /api/todo/ton_id
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            TodoDto todo = await getTodoUseCase.Execute(id);
            return Ok(todo);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{id}")] // /api/todo/ton_id
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            TodoDto todo = await deleteUseCase.Execute(id);
            return Ok(todo);
        }
        catch (NotFoundException)
        {
            return NoContent();
        }
    }

    [HttpPatch("{id}")] // /api/todo/ton_id
    public async Task<IActionResult> ToggleCompleted(Guid id)
    {
        try
        {
            TodoDto todo = await toggleTodoCompleteStatusUseCase.Execute(id);
            return Ok(todo);
        }
        catch (NotFoundException)
        {
            return NoContent();
        }
    }
    // Pour le delete et le update, tu peux retourn un noContent (http 204) qui dit :"Ça fonctionné, je n'ai rien à te retourner"
    //return NoContent();
}