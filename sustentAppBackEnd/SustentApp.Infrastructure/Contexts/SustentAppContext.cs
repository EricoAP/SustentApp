using Microsoft.EntityFrameworkCore;
using SustentApp.Domain.Users.Entities;
using SustentApp.Infrastructure.Users.Configurations;

namespace SustentApp.Infrastructure.Contexts;

public class SustentAppContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public SustentAppContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
