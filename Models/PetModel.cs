using System.ComponentModel.DataAnnotations;

namespace Pets.Models;

public class PetModel
{
    public Guid Id {get; set;}
    public required string petName { get; set; }

    public required string ownerName { get; set; }

    public string? Breed { get; set; }

    public int? Age { get; set; }

    public string? Address {get; set;}

}
