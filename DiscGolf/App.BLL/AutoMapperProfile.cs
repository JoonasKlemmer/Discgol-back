using AutoMapper;
namespace App.BLL;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.DAL.DTO.Wishlist, App.BLL.DTO.Wishlist>().ReverseMap();
        CreateMap<App.DAL.DTO.Category, App.BLL.DTO.Category>().ReverseMap();
        CreateMap<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer>().ReverseMap();
    }
}