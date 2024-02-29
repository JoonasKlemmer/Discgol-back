using Microsoft.AspNetCore.Identity;

namespace App.Domain;

public class Wishlist
{
    public Guid Id { get; set; }
    
    
    public Guid UsersId { get; set; }
    public IdentityUser? User { get; set; }



}