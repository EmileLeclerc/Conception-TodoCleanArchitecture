using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class CreateUseCase
{
    private readonly IShipRepository _shipRepository;

    public CreateUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<ShipDTO> Execute(CreateShipDTO createShipDTO)
    {
        Ship shipToCreate = new Ship(createShipDTO.Name, createShipDTO.GoldCargo, DateTime.UtcNow, createShipDTO.Captain,  createShipDTO.Status, createShipDTO.CrewSize, createShipDTO.CreatedBy);
        Ship? ship = await _shipRepository.Add(shipToCreate);
        return new ShipDTO(ship);
    }
}