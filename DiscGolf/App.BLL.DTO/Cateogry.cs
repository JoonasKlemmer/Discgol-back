using System.ComponentModel.DataAnnotations;
using App.Domain;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Category : IDomainEntityId
{
    public Guid Id { get; set; }
    [MaxLength(128)]
    public string CategoryName { get; set; } = default!;
    
}