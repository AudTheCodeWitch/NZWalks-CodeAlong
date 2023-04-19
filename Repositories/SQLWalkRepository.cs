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

    public async Task<List<Walk?>> GetAllAsync(string? filterOn = null,
        string? filterQuery = null,
        string? sortBy = null,
        bool isAscending = true, int? pageNumber = 1, int? pageSize = 1000)
    {
        var walks = _dbContext.Walks
            .Include("Difficulty")
            .Include("Region")
            .AsQueryable();

        // Filtering
        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                walks = walks.Where(x => x.Name.Contains(filterQuery));

        // Sorting
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                if (isAscending)
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }
            else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                if (isAscending)
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKM) : walks.OrderByDescending(x => x.LengthInKM);
            }
        }

        // Pagination
        var skipResults = (pageNumber - 1) * pageSize;

        return await walks.Skip((int)skipResults).Take((int)pageSize).ToListAsync();
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