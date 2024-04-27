using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.Category, App.DTO.v1_0.Category>().ReverseMap();
        CreateMap<App.BLL.DTO.Disc, App.DTO.v1_0.Disc>().ReverseMap();
        CreateMap<App.BLL.DTO.DiscFromPage, App.DTO.v1_0.DiscFromPage>().ReverseMap();
        CreateMap<App.BLL.DTO.DiscsInWishlist, App.DTO.v1_0.DiscsInWishlist>().ReverseMap();
        CreateMap<App.BLL.DTO.Manufacturer, App.DTO.v1_0.Manufacturer>().ReverseMap();
        CreateMap<App.BLL.DTO.Price, App.DTO.v1_0.Price>().ReverseMap();
        CreateMap<App.BLL.DTO.Website, App.DTO.v1_0.Website>().ReverseMap();
        CreateMap<App.BLL.DTO.Wishlist, App.DTO.v1_0.Wishlist>().ReverseMap();
    }
}