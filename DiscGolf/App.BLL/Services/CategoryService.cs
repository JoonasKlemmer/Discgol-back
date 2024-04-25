using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;

using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;
using Category = App.BLL.DTO.Category;


namespace App.BLL.Services;

public class CategoryService :
    BaseEntityService<App.DAL.DTO.Category, App.BLL.DTO.Category, ICategoryRepository>,
    ICategoryService
{
    public CategoryService(IUnitOfWork uoW, ICategoryRepository repository, IMapper mapper) : base(uoW,
        repository, new BllDalMapper<App.DAL.DTO.Category, App.BLL.DTO.Category>(mapper))
    {
    }

    public async Task<IEnumerable<Category>> GetAllSortedAsync(Guid userId)
    {
        return (await Repository.GetAllSortedAsync(userId)).Select(e => Mapper.Map(e));
    }
}