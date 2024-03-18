using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ICategoryRepository : IEntityRepository<Category>
{
    // define your custom methods here
}