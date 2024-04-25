using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Category, App.DAL.DTO.Category>().ReverseMap();
        CreateMap<App.Domain.Disc, App.DAL.DTO.Disc>().ReverseMap();
        CreateMap<App.Domain.DiscFromPage, App.DAL.DTO.DiscFromPage>().ReverseMap();
        CreateMap<App.Domain.DiscsInWishlist, App.DAL.DTO.DiscsInWishlist>().ReverseMap();
        CreateMap<App.Domain.Manufacturer, App.DAL.DTO.Manufacturer>().ReverseMap();
        CreateMap<App.Domain.Price, App.DAL.DTO.Price>().ReverseMap();
        CreateMap<App.Domain.Website, App.DAL.DTO.Website>().ReverseMap();
        CreateMap<App.Domain.Wishlist, App.DAL.DTO.Wishlist>().ReverseMap();
    }
}