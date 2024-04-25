using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.Domain;

public class Price : IDomainEntityId
{
    public Guid Id { get; set; }
    
    [MaxLength(30)]
    public string Currency { get; set; } = default!;
    
    [MaxLength(5)]
    public string Iso { get; set; } = default!;
    
    [MaxLength(5)]
    public string Symbol { get; set; } = default!;
    
    public ICollection<DiscFromPage>? DiscsFromPages { get; set; }

}