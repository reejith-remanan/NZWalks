using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Difficulty>> GetAllAsync()
        {
            var response = await dBContext.Difficulties.ToListAsync();
            return (response); 
        }
    
    }
}
