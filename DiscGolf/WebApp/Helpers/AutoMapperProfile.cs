using AutoMapper;

namespace WebApp.Helpers;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.BLL.DTO.Wishlist, App.DTO.v1_0.Wishlist>().ReverseMap();
        CreateMap<App.BLL.DTO.Manufacturer, App.DTO.v1_0.Manufacturer>().ReverseMap();
        CreateMap<App.BLL.DTO.Category, App.DTO.v1_0.Category>().ReverseMap();
    }
}