using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

public class ShipRepository : IShipRepository
{
    private readonly AppDbContext _context;
    public ShipRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ship> Add(Ship ship)
    {
        EntityEntry<Ship> newShip = await _context.Ships.AddAsync(ship); // appelle la méthode AddAsync
        await _context.SaveChangesAsync(); // sauvegarde les changements dans la base de données
        return newShip.Entity; // retourne l'entité ajoutée.
    }
    public async Task<Ship?> GetById(Guid id)
    {
        return await _context.Ships

            .SingleOrDefaultAsync(s => s.Id == id);
    }
    public async Task<List<Ship>> GetAll()
    {
        return await _context.Ships.ToListAsync();
    }
    public async Task<Ship?> Delete(Guid id)
    {
        Ship? x = null;
        await _context.Ships.Where(x => x.Id == id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
        return x;
    }
}

