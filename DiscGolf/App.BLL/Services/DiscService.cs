using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;

using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Disc = App.BLL.DTO.Disc;


namespace App.BLL.Services;

public class DiscService :
    BaseEntityService<App.DAL.DTO.Disc, App.BLL.DTO.Disc, IDiscRepository>,
    IDiscService
{
    public DiscService(IUnitOfWork uoW, IDiscRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Disc, App.BLL.DTO.Disc>(mapper))
    {
    }

    public async Task<IEnumerable<Disc>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }
}