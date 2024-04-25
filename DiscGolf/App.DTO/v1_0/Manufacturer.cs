using System.ComponentModel.DataAnnotations;
using App.Domain;


namespace App.DTO.v1_0;

public class Manufacturer
{
    public Guid Id { get; set; }
    
    [MaxLength(64)]
    public string ManufacturerName { get; set; } = default!;
    
    [MaxLength(64)]
    public string Location { get; set; } = default!;
    
    public ICollection<Disc>? Discs { get; set; }
}