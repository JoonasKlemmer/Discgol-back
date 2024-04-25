using System.ComponentModel.DataAnnotations;
using App.Domain;

namespace App.DTO.v1_0;

public class Category
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<Disc>? Discs { get; set; }
}