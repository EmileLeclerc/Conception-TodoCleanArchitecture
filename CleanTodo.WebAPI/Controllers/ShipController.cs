
using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOs;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/ship")]
public class ShipController(GetAllUseCase getAllUseCase, GetUseCase getUseCase, CreateUseCase createUseCase, DeleteUseCase deleteUseCase, EditUseCase editUseCase, IConfiguration configuration) : ControllerBase
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

    [HttpDelete("delete/{id:guid}")]
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

    [HttpPatch("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, Ship ship)
    {
        try
        {
            ShipDTO newship = await editUseCase.Execute(id, ship);
            return Ok(newship);
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

    [HttpGet("broker-users")]
    public async Task<IActionResult> GetBrokerUsers(
      [FromServices] IHttpClientFactory httpClientFactory)
    {
        using var client = httpClientFactory.CreateClient("Broker");

        var clientId = configuration["Broker:AppId"];
        var appToken = configuration["Broker:AppSecret"];

        if (string.IsNullOrEmpty(clientId) ||
            string.IsNullOrEmpty(appToken))
        {
            return StatusCode(500, new
            {
                error = "no appid or secret"
            });
        }

        using var request = new HttpRequestMessage(
          HttpMethod.Get,
          "/api/users");

        request.Headers.Add("x-client-id", clientId);
        request.Headers.Authorization =
          new System.Net.Http.Headers.AuthenticationHeaderValue(
            "Bearer",
            appToken);

        using var response = await client.SendAsync(request);
        var body = await response.Content.ReadAsStringAsync();

        return StatusCode((int)response.StatusCode, new
        {
            brokerStatus = (int)response.StatusCode,
            brokerResponse = body
        });
    }

}