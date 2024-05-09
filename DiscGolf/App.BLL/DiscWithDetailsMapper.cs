using App.BLL.DTO;
using Disc = App.BLL.DTO.Disc;
using DiscFromPage = App.BLL.DTO.DiscFromPage;


namespace App.BLL;

public class DiscWithDetailsMapper
{
  
    public DiscWithDetails MapDisc(Disc? disc,
        DiscFromPage page,string website,
        string manufacturers, string categories)
    {
        var discWithDetails = new DiscWithDetails
        {
            DiscName = disc!.Name,
            Speed = disc.Speed,
            Glide = disc.Glide,
            Turn = disc.Turn,
            Fade = disc.Fade,
            ManufacturerName = manufacturers,
            CategoryName = categories,
            DiscPrice = page.Price,
            PageUrl = website
        };
        return discWithDetails;
    }
}