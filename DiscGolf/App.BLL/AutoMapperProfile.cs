using AutoMapper;
namespace App.BLL;
// Trying to push from other PC
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
        CreateMap<App.DAL.DTO.Price, App.BLL.DTO.Price>().ReverseMap();
        CreateMap<App.DAL.DTO.Website, App.BLL.DTO.Website>().ReverseMap();
        CreateMap<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscWithDetails>()
            .ForMember(dest => dest.DiscFromPageId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Discs!.Name))
            .ForMember(dest => dest.Speed, opt => opt.MapFrom(src => src.Discs!.Speed))
            .ForMember(dest => dest.Glide, opt => opt.MapFrom(src => src.Discs!.Glide))
            .ForMember(dest => dest.Turn, opt => opt.MapFrom(src => src.Discs!.Turn))
            .ForMember(dest => dest.Fade, opt => opt.MapFrom(src => src.Discs!.Fade))
            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Discs!.Manufacturers!.ManufacturerName))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Discs!.Categories!.CategoryName))
            .ForMember(dest => dest.DiscPrice, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.PageUrl, opt => opt.MapFrom(src => src.Websites!.Url));
        
        CreateMap<App.DAL.DTO.DiscsInWishlist, App.BLL.DTO.DiscWithDetails>()
            .ForMember(dest => dest.DiscFromPageId, opt => opt.MapFrom(src => src.DiscFromPage!.Id))
            .ForMember(dest => dest.DiscsInWishlistId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DiscFromPage!.Discs!.Name))
            .ForMember(dest => dest.Speed, opt => opt.MapFrom(src => src.DiscFromPage!.Discs!.Speed))
            .ForMember(dest => dest.Glide, opt => opt.MapFrom(src => src.DiscFromPage!.Discs!.Glide))
            .ForMember(dest => dest.Turn, opt => opt.MapFrom(src => src.DiscFromPage!.Discs!.Turn))
            .ForMember(dest => dest.Fade, opt => opt.MapFrom(src => src.DiscFromPage!.Discs!.Fade))
            .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.DiscFromPage!.Discs!.Manufacturers!.ManufacturerName))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.DiscFromPage!.Discs!.Categories!.CategoryName))
            .ForMember(dest => dest.DiscPrice, opt => opt.MapFrom(src => src.DiscFromPage!.Price))
            .ForMember(dest => dest.PageUrl, opt => opt.MapFrom(src => src.DiscFromPage!.Websites!.Url));
    }
}