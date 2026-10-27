using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class GetAllUseCase
{
    private readonly IShipRepository _shipRepository;

    public GetAllUseCase(IShipRepository shipRepository)
    {
        _shipRepository = shipRepository;
    }

    public async Task<IList<ShipDTO>> Execute()
    {
        var ships = await _shipRepository.GetAll();
        return ships.Select(x => new ShipDTO(x)).ToList();
    }
}