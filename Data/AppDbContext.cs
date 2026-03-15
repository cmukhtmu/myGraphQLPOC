using GraphQLPeopleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLPeopleApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Person> People => Set<Person>();
    public DbSet<AddressHistory> AddressHistory => Set<AddressHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasMany(p => p.AddressHistory)
            .WithOne(a => a.Person!)
            .HasForeignKey(a => a.PeopleId);
    }
}
