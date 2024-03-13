using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain;

public class Wishlist : BaseEntityId, IDomainAppUser<AppUser>
{
    [MaxLength(128)]
    public string WishlistName { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}