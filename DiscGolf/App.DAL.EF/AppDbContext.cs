using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>

{
    public DbSet<Category> Category { get; set; } = default!;
    public DbSet<Disc> Disc { get; set; } = default!;
    public DbSet<DiscFromPage> DiscsFromPage { get; set; } = default!;
    public DbSet<DiscsInWishlist> DiscInWishlist { get; set; } = default!;
    public DbSet<Manufacturer> Manufacturer { get; set; } = default!;
    public DbSet<Price> Price { get; set; } = default!;
    public DbSet<Website> Website { get; set; } = default!;
    public DbSet<Wishlist> Wishlist { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
}