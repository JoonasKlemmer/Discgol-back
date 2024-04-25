using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.Domain;

public class Disc : IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public double Speed { get; set; }
    public double Glide { get; set; }
    public double Turn { get; set; }
    public double Fade { get; set; }
    
    public Guid ManufacturerId { get; set; }
    public Manufacturer? Manufacturer { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category? Categories { get; set; }
    
    
    
    public ICollection<DiscFromPage>? DiscsFromPages { get; set; }
    public ICollection<DiscsInWishlist>? DiscsInWishlists { get; set; }

}