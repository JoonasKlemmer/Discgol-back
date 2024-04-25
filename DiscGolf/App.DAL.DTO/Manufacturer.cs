using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class Manufacturer : IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(64)]
    public string ManufacturerName { get; set; } = default!;
    
    [MaxLength(64)]
    public string Location { get; set; } = default!;
    
    public ICollection<Disc>? Discs { get; set; }

}