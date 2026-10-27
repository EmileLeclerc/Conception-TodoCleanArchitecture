using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class DeleteUseCase
{
    private readonly IShipRepository _shipRepository;

    public DeleteUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<ShipDTO> Execute(Guid id)
    {
        Ship? ship = await _shipRepository.Delete(id);
        if (ship == null)
        {
            throw new NotFoundException(id);
        }
        return new ShipDTO(ship);
    }
}