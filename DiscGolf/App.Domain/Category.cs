using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.Domain;

public class Category : IDomainEntityId
{
    public Guid Id { get; set; }
    [MaxLength(64)]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<Disc>? Discs { get; set; }
}