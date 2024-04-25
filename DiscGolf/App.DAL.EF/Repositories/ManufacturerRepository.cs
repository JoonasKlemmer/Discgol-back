using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ManufacturerRepository : BaseEntityRepository<APPDomain.Manufacturer, DALDTO.Manufacturer, AppDbContext>, IManufacturerRepository
{
    public ManufacturerRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.Manufacturer, DALDTO.Manufacturer>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.Manufacturer>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.OrderBy(c => c.ManufacturerName);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }

    
    // implement your custom methods here
}