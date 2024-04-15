using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NXWalks.API.Data;
using NXWalks.API.Models.Domains;

namespace NXWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NXWalksDBContext _dBContext;

        public SQLRegionRepository(NXWalksDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _dBContext.Regions.AddAsync(region);
            await _dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await _dBContext.Regions.FirstOrDefaultAsync(x => x.ID == id);

            if (existingRegion == null) {

                return null;
            }

            _dBContext.Regions.Remove(existingRegion);
            await _dBContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
          return await _dBContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByID(Guid id)
        {
            return await _dBContext.Regions.FirstOrDefaultAsync(r => r.ID == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _dBContext.Regions.FirstOrDefaultAsync(x=> x.ID == id);
            if(existingRegion == null)
            {
                return null;

            }

            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImgURL = region.RegionImgURL;

            await _dBContext.SaveChangesAsync();

            return existingRegion;

        }
    }
}
