using NXWalks.API.Models.Domains;

namespace NXWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByID(Guid id);

        Task<Region>CreateAsync(Region region);

        Task<Region?>UpdateAsync(Guid id, Region region);

        Task<Region?>DeleteAsync(Guid id);
    }
}
