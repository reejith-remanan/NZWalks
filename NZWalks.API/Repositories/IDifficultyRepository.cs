using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync();

        Task<Difficulty?> GetByIdAsync(Guid id);

        Task<Difficulty?> DeleteAsync(Guid id);

        Task<Difficulty> CreateAsync(Difficulty difficulty);

        Task<Difficulty> UpdateAsync(Difficulty difficulty, Guid id);
    }
}
