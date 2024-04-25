using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;

using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Website = App.BLL.DTO.Website;


namespace App.BLL.Services;

public class WebsiteService :
    BaseEntityService<App.DAL.DTO.Website, App.BLL.DTO.Website, IWebsiteRepository>,
    IWebsiteService
{
    public WebsiteService(IUnitOfWork uoW, IWebsiteRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Website, App.BLL.DTO.Website>(mapper))
    {
    }

    public async Task<IEnumerable<Website>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }
}