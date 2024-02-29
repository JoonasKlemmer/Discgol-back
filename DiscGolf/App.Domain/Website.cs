using System.ComponentModel.DataAnnotations;

namespace App.Domain;

public class Website
{
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string Url { get; set; } = default!;
    [MaxLength(64)]
    public string WebsiteName { get; set; } = default!;
    
    public ICollection<DiscFromPage>? DiscsFromPage { get; set; }

}