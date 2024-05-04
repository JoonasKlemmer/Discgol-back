namespace App.DTO.v1_0;

public class DiscWithDetails
{
    
    public string DiscName { get; set; } = default!;
    
    public double Speed { get; set; }
    public double Glide { get; set; }
    public double Turn { get; set; }
    public double Fade { get; set; }
    
    public string? ManufacturerName { get; set; } = default!;
    public string? CategoryName { get; set; } = default!;
    public double? DiscPrice { get; set; } = default!;
    public string? PageUrl { get; set; } = default!;
    
}