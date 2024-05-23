namespace App.BLL.DTO;

public class DiscWithDetails
{
    public Guid DiscFromPageId { get; set; }
    public Guid DiscsInWishlistId { get; set; } = Guid.Empty;
    public string Name { get; set; } = default!;
    
    public double Speed { get; set; }
    public double Glide { get; set; }
    public double Turn { get; set; }
    public double Fade { get; set; }
    
    public string? ManufacturerName { get; set; } = default!;
    public string? CategoryName { get; set; } = default!;
    public double? DiscPrice { get; set; } = default!;
    public string? PageUrl { get; set; } = default!;
    public string? PictureUrl { get; set; } = default!;
    
}