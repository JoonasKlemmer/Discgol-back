using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using DiscFromPage = App.BLL.DTO.DiscFromPage;


namespace App.BLL.Services;

public class DiscFromPageService :
    BaseEntityService<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscFromPage, IDiscFromPageRepository>,
    IDiscFromPageService
{
    public DiscFromPageService(IUnitOfWork uoW, IDiscFromPageRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscFromPage>(mapper))
    {
    }

    public async Task<IEnumerable<DiscFromPage>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }
}