using AutoMapper;
namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.Category, App.BLL.DTO.Category>().ReverseMap();
        CreateMap<App.DAL.DTO.Wishlist, App.BLL.DTO.Wishlist>().ReverseMap();
        CreateMap<App.DAL.DTO.Disc, App.BLL.DTO.Disc>().ReverseMap();
        CreateMap<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscFromPage>().ReverseMap();
        CreateMap<App.DAL.DTO.DiscsInWishlist, App.BLL.DTO.DiscsInWishlist>().ReverseMap();
        CreateMap<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer>().ReverseMap();
        CreateMap<App.DAL.DTO.DiscWithDetails, App.BLL.DTO.DiscWithDetails>().ReverseMap();
        CreateMap<App.DAL.DTO.Price, App.BLL.DTO.Price>().ReverseMap();
        CreateMap<App.DAL.DTO.Website, App.BLL.DTO.Website>().ReverseMap();
    }
}