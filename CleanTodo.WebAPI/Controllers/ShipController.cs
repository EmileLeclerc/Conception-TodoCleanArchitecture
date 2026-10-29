using System.Net.Http.Headers;
using System.Text.Json;
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
    public async Task<IActionResult> GetBrokerUsers([FromServices] IHttpClientFactory httpClientFactory)
    {
        var client = httpClientFactory.CreateClient("Broker");

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

        var request = new HttpRequestMessage(
          HttpMethod.Get,
          "/api/users");

        request.Headers.Add("x-client-id", clientId);
        request.Headers.Authorization =
          new AuthenticationHeaderValue("Bearer",appToken);

        var response = await client.SendAsync(request);
        var body = await response.Content.ReadAsStringAsync();

        return StatusCode((int)response.StatusCode, new
        {
            brokerStatus = (int)response.StatusCode,
            brokerResponse = body
        });
    }

    [HttpPost("sail/{user}/{id:guid}/")]
    public async Task<IActionResult> Sail(Guid id, string user, [FromServices] IHttpClientFactory httpClientFactory)
    {

        var appId = configuration["Broker:AppId"];
        var appSecret = configuration["Broker:AppSecret"];

        if (string.IsNullOrEmpty(appId) ||
            string.IsNullOrEmpty(appSecret))
        {
            return StatusCode(500, new
            {
                error = "no appid or secret"
            });
        }

        if (string.IsNullOrEmpty(user))
        {
            return StatusCode(500, new { error = "no user to sail to" });
        }

        var client = httpClientFactory.CreateClient("Broker");

        var request = new HttpRequestMessage(
          HttpMethod.Get,
          "/api/users");

        request.Headers.Add("x-client-id", appId);
        request.Headers.Authorization =
          new AuthenticationHeaderValue("Bearer",appSecret);

        var usersResponse = await client.SendAsync(request);
        var usersBody = await usersResponse.Content.ReadAsStringAsync();
        ShipDTO? ship;
        try
        {
             ship = await getUseCase.Execute(id);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        var outgoingShip = new SendShipDTO(ship);

        using var sendRequest = new HttpRequestMessage(
          HttpMethod.Post,
          $"/api/ship/sail/{Uri.EscapeDataString(user)}");

        sendRequest.Headers.Add("x-client-id", appId);
        sendRequest.Headers.Authorization =
          new AuthenticationHeaderValue("Bearer", appSecret);

        sendRequest.Content = new StringContent(
          JsonSerializer.Serialize(outgoingShip,
            new JsonSerializerOptions(JsonSerializerDefaults.Web)),
          System.Text.Encoding.UTF8,
          "application/json");
        using var Response = await client.SendAsync(sendRequest);
        var sendBody = await Response.Content.ReadAsStringAsync();

        return StatusCode((int)Response.StatusCode, new
        {
            brokerStatus = (int)Response.StatusCode,
            brokerResponse = sendBody
        });
    }
}