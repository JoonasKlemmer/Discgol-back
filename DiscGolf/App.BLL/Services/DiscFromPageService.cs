using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using DiscFromPage = App.BLL.DTO.DiscFromPage;
using DiscWithDetails = App.BLL.DTO.DiscWithDetails;


namespace App.BLL.Services;

public class DiscFromPageService :
    BaseEntityService<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscFromPage, IDiscFromPageRepository>,
    IDiscFromPageService
{
    private readonly IMapper _mapperForDiscWithDetails;
    public DiscFromPageService(IUnitOfWork uoW, IDiscFromPageRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscFromPage>(mapper))
    {
        var configForDiscWithDetails = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<App.DAL.DTO.DiscWithDetails, App.BLL.DTO.DiscWithDetails>();
        });

        _mapperForDiscWithDetails = configForDiscWithDetails.CreateMapper();
    }

    public async Task<IEnumerable<DiscFromPage>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e))!;
    }
    public async Task<IEnumerable<DiscFromPage>> GetAllWithDetails()
    {
        return ( await Repository.GetAllWithDetails()).Select(e => Mapper.Map(e))!;
    }
    
    public async Task<IEnumerable<DiscFromPage>> GetAllWithDetailsByName(string discName)
    {
        return ( await Repository.GetAllWithDetailsByName(discName)).Select(e => Mapper.Map(e))!;
    }
    public async Task<List<App.BLL.DTO.DiscWithDetails>> GetAllDiscData(List<App.BLL.DTO.DiscFromPage> discFromPages)
    {
        var dwdMapper = new DiscWithDetailsMapper();
        //var discFromPages = await GetAllWithDetails();
        var result = discFromPages.ToList();
        var discWd = new List<App.BLL.DTO.DiscWithDetails>();
        foreach (var discFromPage in result)
        {
            var currentDisc = discFromPage!.Discs;
            var website = discFromPage!.Websites!.Url;
            var manufacturerName = currentDisc!.Manufacturer!.ManufacturerName;
            var categoryName = currentDisc!.Categories!.CategoryName;
            var res = dwdMapper.MapDisc(currentDisc,
                discFromPage!,
                website, manufacturerName,
                categoryName);

            discWd.Add(res);
            
        }
        return discWd;
    }
}