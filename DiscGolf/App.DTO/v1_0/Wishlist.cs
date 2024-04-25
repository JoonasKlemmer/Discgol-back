using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1_0;

public class Wishlist
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string WishlistName { get; set; } = default!;
}