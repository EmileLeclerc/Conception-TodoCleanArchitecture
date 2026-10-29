using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Domain.Interfaces.Repositories;

public interface IShipRepository
{
    Task<Ship?> Add(Ship ship);
    Task<Ship?> GetById(Guid id);
    Task<List<Ship>> GetAll();
    Task<Ship?> Delete(Guid id);
    Task<Ship?> Edit(Guid id, Ship ship);
}

