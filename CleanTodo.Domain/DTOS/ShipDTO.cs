
using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOs;

public class ShipDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int GoldCargo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Captain { get; set; }

    public string Status { get; set; }

    public int CrewSize { get; set; }

    public string CreatedBy { get; set; }

    public DateTime LastModified { get; set; } = DateTime.Now;

    public ShipDTO(Ship ship)
    {
        Id = ship.Id;
        Name = ship.Name;
        GoldCargo = ship.GoldCargo;
        CreatedAt = ship.CreatedAt;
        Captain = ship.Captain;
        Status = ship.Status;
        CrewSize = ship.CrewSize;
        CreatedBy = ship.CreatedBy;
    }
    public ShipDTO() { }
}