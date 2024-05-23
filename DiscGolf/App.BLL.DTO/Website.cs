using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Website : IDomainEntityId
{
    public Guid Id { get; set; }


    public string Url { get; set; } = default!;

    public string WebsiteName { get; set; } = default!;
}