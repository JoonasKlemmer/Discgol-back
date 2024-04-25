using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;


namespace App.BLL.Services;

public class ManufacturerService :
    BaseEntityService<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer, IManufacturerRepository>,
    IManufacturerService
{
    public ManufacturerService(IUnitOfWork uoW, IManufacturerRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Manufacturer, App.BLL.DTO.Manufacturer>(mapper))
    {
    }

    public async Task<IEnumerable<Manufacturer>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }
}