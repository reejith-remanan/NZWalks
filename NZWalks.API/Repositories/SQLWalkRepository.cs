using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dBContext;

        public SQLWalkRepository(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        } 
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dBContext.Walks.AddAsync(walk);
            await dBContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            dBContext.Walks.Remove(existingWalk);
            await dBContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
                                                  int pageNumber = 1, int pageSize = 1000)
        {

            var Walks = dBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = Walks.Where(x => x.Name.Contains(filterQuery));
                }
                
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.Name) : Walks.OrderByDescending(x => x.Name);
                }
                else if ((sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase)))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.LenghtInKm) : Walks.OrderByDescending(x => x.LenghtInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;


            return await Walks.Skip(skipResults).Take(pageSize).ToListAsync();


            //return await dBContext.Walks
            //    .Include("Difficulty")
            //    .Include("Region")
            //    .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dBContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dBContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.LenghtInKm = walk.LenghtInKm;
            existingWalk.Description = walk.Description;
            existingWalk.Name = walk.Name;

            await dBContext.SaveChangesAsync();

            return await dBContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
