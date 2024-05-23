using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services
{
    public class DiscFromPageService :
        BaseEntityService<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscFromPage, IDiscFromPageRepository>,
        IDiscFromPageService
    {
        private readonly IMapper _mapper;

        public DiscFromPageService(IUnitOfWork uoW, IDiscFromPageRepository repository, IMapper mapper) : base(uoW,
            repository, new BllDalMapper<App.DAL.DTO.DiscFromPage, App.BLL.DTO.DiscFromPage>(mapper))
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<DiscFromPage>> GetAllSortedAsync(Guid userId)
        {

            var discsFromPage = await Repository.GetAllSortedAsync(userId);


            return discsFromPage.Select(disc => _mapper.Map<DiscFromPage>(disc));
        }

        public async Task<IEnumerable<DiscFromPage>> GetAllWithDetails()
        {
            var discsFromPage = await Repository.GetAllWithDetails();
            
            return discsFromPage.Select(disc => _mapper.Map<DiscFromPage>(disc));
        }

        public async Task<IEnumerable<DiscFromPage>> GetAllWithDetailsByName(string Name)
        {

            var discsFromPage = await Repository.GetAllWithDetailsByName(Name);
            
            return discsFromPage.Select(disc => _mapper.Map<DiscFromPage>(disc));
        }



        public async Task<IEnumerable<DiscFromPage>> GetWithDetailsByDiscId(Guid discId)
        {

            var discsFromPage = await Repository.GetWithDetailsByDiscId(discId);
            
            return discsFromPage.Select(disc => _mapper.Map<DiscFromPage>(disc));
        }



        public async Task<List<DiscWithDetails>> GetAllDiscData(List<DiscFromPage> discFromPages)
        {
            return await Task.Run(() =>
            {
                var discWd = new List<DiscWithDetails>();
                foreach (var discFromPage in discFromPages)
                {
                    var currentDisc = discFromPage.Discs;
                    var website = discFromPage.Websites!.Url;
                    var manufacturerName = currentDisc!.Manufacturers!.ManufacturerName;
                    var categoryName = currentDisc.Categories!.CategoryName;

                    var discWithDetails = new DiscWithDetails
                    {
                        DiscFromPageId = discFromPage.Id,
                        Name = currentDisc.Name,
                        Speed = currentDisc.Speed,
                        Glide = currentDisc.Glide,
                        Turn = currentDisc.Turn,
                        Fade = currentDisc.Fade,
                        ManufacturerName = manufacturerName,
                        CategoryName = categoryName,
                        DiscPrice = discFromPage.Price,
                        PageUrl = website
                    };

                    discWd.Add(discWithDetails);
                }

                return discWd;
            });
        }

    }
}