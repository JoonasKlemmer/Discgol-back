using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class Wishlist : IDomainEntityId
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string WishlistName { get; set; } = default!;

}