
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanTodo.Domain.Entities;

public class Ship
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public int GoldCargo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Captain { get; set; }

    public string Status { get; set; } = "docked";

    public int CrewSize { get; set; }

    public string CreatedBy { get; set; }

    public DateTime LastModified { get; set; } = DateTime.Now;

    public Ship(string name, int goldCargo, DateTime createdAt, string captain, string status, int crewSize, string createdBy)
    {
        Name = name;
        GoldCargo = goldCargo;
        CreatedAt = createdAt;
        Captain = captain;
        Status = status;
        CrewSize = crewSize;
        CreatedBy = createdBy;
        LastModified = DateTime.Now;
    }

    public Ship() { }
}