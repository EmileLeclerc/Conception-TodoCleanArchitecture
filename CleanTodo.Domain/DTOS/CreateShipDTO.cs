
using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS;

public class CreateShipDTO
{
    public string Name { get; set; }
    public int GoldCargo { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Captain { get; set; }
    public string Status { get; set; }
    public int CrewSize { get; set; }
    public string CreatedBy { get; set; }

    public CreateShipDTO() { }

    public CreateShipDTO(Ship ship)
    {
        Name = ship.Name;
        GoldCargo = ship.GoldCargo;
        CreatedAt = ship.CreatedAt;
        Captain = ship.Captain;
        Status = ship.Status;
        CrewSize = ship.CrewSize;
        CreatedBy = ship.CreatedBy;
    }
}
