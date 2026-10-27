using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class GetUseCase
{
    private readonly IShipRepository _shipRepository;

    public GetUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<ShipDTO> Execute(Guid id)
    {
        Ship? ship = await _shipRepository.GetById(id);
        if (ship == null)
        {
            throw new NotFoundException(id);
        }
        return new ShipDTO(ship);
    }
}