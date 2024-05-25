using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Identity;

namespace Base.Test.Domain;

public class TestEntity : BaseEntityId //, IDomainAppUser<IdentityUser<Guid>>, IDomainAppUserId<Guid>
{
    //public Guid AppUserId { get; set; }
    [MaxLength(128)]
    public string Value { get; set; } = default!;
    //public IdentityUser<Guid>? AppUser { get; set; }
}