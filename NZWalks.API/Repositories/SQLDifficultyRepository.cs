using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLDifficultyRepository : IDifficultyRepository
    {

        private readonly NZWalksDBContext dBContext;
        public SQLDifficultyRepository(NZWalksDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Difficulty> CreateAsync(Difficulty difficulty)
        {
            await dBContext.Difficulties.AddAsync(difficulty);
            await dBContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(Guid id)
        {
            var dataExist = await dBContext.Difficulties.FirstOrDefaultAsync(x => x.Id == id);
            if (dataExist != null)
            {
                dBContext.Difficulties.Remove(dataExist);
                await dBContext.SaveChangesAsync();
                return(dataExist);
            }
            return (null);

        }

        public async Task<List<Difficulty>> GetAllAsync()
        {
            var response = await dBContext.Difficulties.ToListAsync();
            return (response); 
        }

        public async Task<Difficulty?> GetByIdAsync(Guid id)
        {
            return(await dBContext.Difficulties.FirstOrDefaultAsync(x=> x.Id == id));
        }

        public async Task<Difficulty> UpdateAsync(Difficulty difficulty, Guid id)
        {
            var existingDifficulty = await dBContext.Difficulties.FirstOrDefaultAsync(x => x.Id ==  id);
            if (existingDifficulty != null)
            {
                existingDifficulty.Name = difficulty.Name;
                await dBContext.SaveChangesAsync();
                return existingDifficulty;
            }
            return null;
        }
    }
}
