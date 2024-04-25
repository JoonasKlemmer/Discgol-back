using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;
namespace App.Contracts.BLL.Services;

public interface ICategoryService : IEntityRepository<App.BLL.DTO.Category>, ICategoryRepositoryCustom<App.BLL.DTO.Category>
{
    
}