using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.DAL.DTO;

public class Website : IDomainEntityId
{
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string Url { get; set; } = default!;
    [MaxLength(64)]
    public string WebsiteName { get; set; } = default!;
    
}