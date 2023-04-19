using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SqlWalkRepository : IWalkRepository
{
    private readonly NzWalksDbContext _dbContext;

    public SqlWalkRepository(NzWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Walk?> CreateAsync(Walk? walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk?>> GetAllAsync()
    {
        return await _dbContext.Walks
            .Include("Difficulty")
            .Include("Region")
            .ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Walks
            .Include("Difficulty")
            .Include("Region")
            .FirstOrDefaultAsync(x => x != null && x.Id == id);
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var walkToUpdate = _dbContext.Walks.FirstOrDefault(x => x != null && x.Id == id);

        if (walkToUpdate == null) return null;

        walkToUpdate.Name = walk.Name;
        walkToUpdate.Description = walk.Description;
        walkToUpdate.DifficultyId = walk.DifficultyId;
        walkToUpdate.RegionId = walk.RegionId;
        walkToUpdate.LengthInKM = walk.LengthInKM;
        walkToUpdate.WalkImageURL = walk.WalkImageURL;

        await _dbContext.SaveChangesAsync();

        return walkToUpdate;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var walkToDelete = await _dbContext.Walks.FirstOrDefaultAsync(x => x != null && x.Id == id);

        if (walkToDelete == null) return null;

        _dbContext.Walks.Remove(walkToDelete);
        await _dbContext.SaveChangesAsync();

        return walkToDelete;
    }
}