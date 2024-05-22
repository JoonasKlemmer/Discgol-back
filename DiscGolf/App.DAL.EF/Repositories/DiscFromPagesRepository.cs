using App.BLL.DTO;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using APPDomain = App.Domain;
using DALDTO = App.DAL.DTO;
using Base.DAL.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class DiscFromPagesRepository : BaseEntityRepository<APPDomain.DiscFromPage, DALDTO.DiscFromPage, AppDbContext>, IDiscFromPageRepository
{
    public DiscFromPagesRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new DalDomainMapper<APPDomain.DiscFromPage, DALDTO.DiscFromPage>(mapper))
    {
    }
    
    public async Task<IEnumerable<DALDTO.DiscFromPage>> GetAllSortedAsync(Guid userId)
    {
        var query = CreateQuery(userId);
        query = query.OrderBy(c => c.Price);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e));
    }
    

    public async Task<IEnumerable<DALDTO.DiscFromPage>> GetAllWithDetails()
    {
        var query = CreateQuery();
        query = query
            .Include(c => c.Discs)
            .ThenInclude(c => c!.Manufacturers)
            .Include(c => c.Discs!.Categories)
            .Include(c => c.Websites)
            .Include(c => c.PriceValue);
        
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e))!;
    }
    
    public async Task<IEnumerable<DALDTO.DiscFromPage>> GetAllWithDetailsByName(string Name)
    {
        var query = CreateQuery();
        
        query = query
            .Include(c => c.Discs)
            .ThenInclude(c => c!.Manufacturers)
            .Include(c => c.Discs!.Categories)
            .Include(c => c.Websites)
            .Include(c => c.PriceValue)
            .Where(c => c.Discs!.Name.ToLower().Contains(Name.ToLower()));
        
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e))!;
    }
    
    public async Task<IEnumerable<DALDTO.DiscFromPage>> GetWithDetailsByDiscId(Guid discId)
    {
        var query = CreateQuery();
        
        query = query
            .Include(c => c.Discs)
            .ThenInclude(c => c!.Manufacturers)
            .Include(c => c.Discs!.Categories)
            .Include(c => c.Websites)
            .Include(c => c.PriceValue)
            .Where(c => c.DiscId == discId);
        var res = await query.ToListAsync();
        return res.Select(e => Mapper.Map(e))!;
    }
}