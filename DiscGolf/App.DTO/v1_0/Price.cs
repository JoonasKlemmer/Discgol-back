using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0;

public class Price
{
    public Guid Id { get; set; }
    
    [MaxLength(30)]
    public string Currency { get; set; } = default!;
    
    [MaxLength(5)]
    public string Iso { get; set; } = default!;
    
    [MaxLength(5)]
    public string Symbol { get; set; } = default!;
}