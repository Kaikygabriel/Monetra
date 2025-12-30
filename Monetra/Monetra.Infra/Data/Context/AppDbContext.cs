using Microsoft.EntityFrameworkCore;
using Monetra.Domain.BackOffice.Entities;
using Monetra.Infra.Data.Mapping;

namespace Monetra.Infra.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext>options) : DbContext(options)
{
    public DbSet<Customer>Customers { get; set; }
    public DbSet<User>Users { get; set; }
    public DbSet<Portfolio>Portfolios { get; set; }
    public DbSet<Transaction>Transactions { get; set; }
    public DbSet<Mark>Marks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MarkMapping());
        modelBuilder.ApplyConfiguration(new CustomerMapping());
        modelBuilder.ApplyConfiguration(new PortfolioMapping());
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new TransactionMapping());

    }
}