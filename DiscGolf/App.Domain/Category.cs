using System.ComponentModel.DataAnnotations;

namespace App.Domain;

public class Category
{
    public Guid Id { get; set; }
    [MaxLength(64)]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<Disc>? Discs { get; set; }
}