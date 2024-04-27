namespace App.DTO.v1_0;

public class DiscsInWishlist
{
    public Guid Id { get; set; }
    
    public Guid DiscId { get; set; }
    public Disc? Discs { get; set; }
    
    public Guid WishlistId { get; set; }
    public Wishlist? Wishlists { get; set; }
}