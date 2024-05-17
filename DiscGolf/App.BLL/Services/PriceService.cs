using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Price = App.BLL.DTO.Price;


namespace App.BLL.Services;

public class PriceService :
    BaseEntityService<App.DAL.DTO.Price, App.BLL.DTO.Price, IPriceRepository>,
    IPriceService
{
    public PriceService(IUnitOfWork uoW, IPriceRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Price, App.BLL.DTO.Price>(mapper))
    {
    }

    public async Task<IEnumerable<Price>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e))!;
    }
    
}