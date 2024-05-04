using App.DTO.v1_0;

namespace WebApp.Models;
/// <summary>
/// Model for search
/// </summary>
public class SearchModel
{
    /// <summary>
    /// List of discs
    /// </summary>
    public List<DiscWithDetails>? DiscWithDetails { get; set; }

}