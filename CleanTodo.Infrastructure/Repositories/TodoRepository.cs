using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Todo>> GetAll()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todo> Add(Todo todo)
    {
        EntityEntry<Todo> newTodo = await _context.Todos.AddAsync(todo); // appelle la méthode AddAsync
        await _context.SaveChangesAsync(); // sauvegarde les changements dans la base de données
        return newTodo.Entity; // retourne l'entité ajoutée.
    }

    public async Task<Todo?> FindById(Guid id)
    {
        return await _context.Todos
            
            .SingleOrDefaultAsync();
    }

    public async Task<Todo?> Delete(Guid id)
    {
        Todo? x = null;
        await _context.Todos.Where(x => x.Id == id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
        return x;
    }

    public async Task<Todo?> ToggleCompleted(Guid id)
    {
        Todo? x = null;
        if(FindById(id).IsCompleted)
        await _context.Todos
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.IsCompleted, false));
        else
        await _context.Todos
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(u => u.IsCompleted, true));
        await _context.SaveChangesAsync();
        return x;
    }
}
