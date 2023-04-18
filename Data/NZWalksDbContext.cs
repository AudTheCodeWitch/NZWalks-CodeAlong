using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data;

public class NZWalksDbContext : DbContext
{
    public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Difficulty> Difficulties { get; set; } // DbSet is a collection of entities
    public DbSet<Region?> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}