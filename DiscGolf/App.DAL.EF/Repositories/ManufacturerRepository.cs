using App.Contracts.DAL.Repositories;
using App.Domain;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ManufacturerRepository : BaseEntityRepository<Manufacturer, Manufacturer, AppDbContext>, IManufacturerRepository
{
    public ManufacturerRepository(AppDbContext dbContext) : 
        base(dbContext, new DalDummyMapper<Manufacturer, Manufacturer>())
    {
    }
    
    // implement your custom methods here
}