using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class EditUseCase
{
    private readonly IShipRepository _shipRepository;

    public EditUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<ShipDTO> Execute(Guid id, Ship ship)
    {
        Ship? existingShip = await _shipRepository.GetById(id);

        if (existingShip == null)
        {
            throw new NotFoundException(id);
        }

        existingShip.Name = ship.Name;
        existingShip.GoldCargo = ship.GoldCargo;
        existingShip.Captain = ship.Captain;
        existingShip.Status = ship.Status;
        existingShip.CrewSize = ship.CrewSize;
        existingShip.LastModified = DateTime.Now;

        Ship? editedShip = await _shipRepository.Edit(id, existingShip);

        if (editedShip == null)
        {
            throw new NotFoundException(id);
        }

        return new ShipDTO(editedShip);
    }
}