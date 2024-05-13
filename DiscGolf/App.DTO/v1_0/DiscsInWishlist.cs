namespace App.DTO.v1_0;

public class DiscsInWishlist
{
    public Guid Id { get; set; }
    
    public Guid DiscFromPageId { get; set; }
    public DiscFromPage? DiscFromPage { get; set; }
    
    public Guid WishlistId { get; set; }
    public Wishlist? Wishlists { get; set; }
}