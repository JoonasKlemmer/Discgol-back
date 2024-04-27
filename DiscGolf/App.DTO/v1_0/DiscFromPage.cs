namespace App.DTO.v1_0;

public class DiscFromPage
{
    public Guid Id { get; set; }
    
    public double Price { get; set; }
    
    
    public Guid DiscId { get; set; }
    public Disc? Discs { get; set; }
    
    public Guid WebsiteId { get; set; }
    public Website? Websites { get; set; }
}