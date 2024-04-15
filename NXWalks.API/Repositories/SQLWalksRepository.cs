using Microsoft.EntityFrameworkCore;
using NXWalks.API.Data;
using NXWalks.API.Models.Domains;

namespace NXWalks.API.Repositories
{
    public class SQLWalksRepository : IWalksRepositories
    {
        private readonly NXWalksDBContext nXWalksDBContext;

        public SQLWalksRepository(NXWalksDBContext nXWalksDBContext)
        {
            this.nXWalksDBContext = nXWalksDBContext;
        }
        public async Task<Walks> CreateAsync(Walks walks)
        {
               await nXWalksDBContext.Walks.AddAsync(walks);
               await nXWalksDBContext.SaveChangesAsync();
               return walks;
        }

        public async Task<Walks?> DeleteWalk(Guid id)
        {
           var deleteexist = await nXWalksDBContext.Walks.FirstOrDefaultAsync(x => x.ID == id);
            if (deleteexist == null)
            {
                return null;
            }
            nXWalksDBContext.Walks.Remove(deleteexist);
            await nXWalksDBContext.SaveChangesAsync();
            return deleteexist;
        }

        public async Task<List<Walks>> GetAllWalksAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAssending = true, int pageNumber = 1, int pageSize = 100)
        {
            var walks = nXWalksDBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filtering
            //FIrst check that if filterOn and filterQuery is null or have some value 
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                //Now checking that on which column the fitering is being applied 
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    //Checking here that if on the filterOn contins that search items through filter query
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAssending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name) ;
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase) == false)
                {
                    walks = isAssending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination

            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

          //Method just bring the All the List That is present 
          //var walksDomainModel = await nXWalksDBContext.Walks
          //      .Include("Difficulty")
          //      .Include("Region")
          //      .ToListAsync();
          //return walksDomainModel;
        }

        public async Task<Walks?> GetWalkByID(Guid id)
        {
          return await nXWalksDBContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Walks?> UpdateWalkAsync(Guid id, Walks walks)
        {
            var walkExist = await nXWalksDBContext.Walks.FirstOrDefaultAsync(x => x.ID == id);

            if(walkExist == null)
            {
                return null;
            }

            walkExist.Name = walks.Name;
            walkExist.Description = walks.Description;
            walkExist.LengthInKm = walks.LengthInKm;
            walkExist.WalkImgURL = walks.WalkImgURL;
            walkExist.RegionID = walks.RegionID;
            walkExist.DifficultyID = walks.DifficultyID;

            await nXWalksDBContext.SaveChangesAsync();

            return walkExist;
        }
    }
}
