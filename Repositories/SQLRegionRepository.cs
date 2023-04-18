using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SqlRegionRepository : IRegionRepository
{
    private readonly NzWalksDbContext _dbContext;

    public SqlRegionRepository(NzWalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Region?>> GetAllAsync()
    {
        return await _dbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Regions.FirstOrDefaultAsync(x => x != null && x.Id == id);
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _dbContext.Regions.AddAsync(region);
        await _dbContext.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var regionToUpdate = await _dbContext.Regions.FirstOrDefaultAsync(x => x != null && x.Id == id);
        if (regionToUpdate == null) return null;

        regionToUpdate.Code = region.Code;
        regionToUpdate.Name = region.Name;
        regionToUpdate.RegionImageURL = region.RegionImageURL;

        await _dbContext.SaveChangesAsync();
        return regionToUpdate;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var regionToDelete = await _dbContext.Regions.FirstOrDefaultAsync(x => x != null && x.Id == id);

        if (regionToDelete == null) return null;

        _dbContext.Regions.Remove(regionToDelete);
        await _dbContext.SaveChangesAsync();

        return regionToDelete;
    }
}