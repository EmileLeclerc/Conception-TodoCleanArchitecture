
using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ship")]
public class ShipController(GetAllUseCase getAllUseCase, GetUseCase getUseCase, CreateUseCase createUseCase, DeleteUseCase deleteUseCase) : ControllerBase
{


    [HttpPost("dock")]
    public async Task<ActionResult<ShipDTO>> Dock([FromBody] CreateShipDTO createShipDTO)
    {
        if (createShipDTO.Status != "sailing")
        {
            return BadRequest(new
            {
                error = "this ship is not sailing what"
            });
        }

        ShipDTO ship = await createUseCase.Execute(createShipDTO);

        return CreatedAtAction(
            nameof(Get),
            new { id = ship.Id },
            ship);
    
    }

    [HttpGet("getall")]
    public async Task<ActionResult<IEnumerable<ShipDTO>>> GetAll()
    {
        var ships = await getAllUseCase.Execute();
        return Ok(ships);
    }

    [HttpPost("create")]
    public async Task<ActionResult<ShipDTO>> Create([FromBody] CreateShipDTO createShipDTO)
    {
        ShipDTO ship = await createUseCase.Execute(createShipDTO);

        return CreatedAtAction(
            nameof(Get),
            new { id = ship.Id },
            ship);
    }

    [HttpGet("get/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            ShipDTO ship = await getUseCase.Execute(id);
            return Ok(ship);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("delete{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            ShipDTO ship = await deleteUseCase.Execute(id);
            return Ok(ship);
        }
        catch (NotFoundException)
        {
            return NoContent();
        }
    }

    [HttpGet("/api/ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}