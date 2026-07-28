using Microsoft.EntityFrameworkCore;
using FinanceService.Domain.Entities;

namespace FinanceService.Infrastructure.DataAccess;

internal class PersonalFinanceDbContext(DbContextOptions options) : DbContext(options: options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Pot> Pots { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Bill> Bills { get; set; }
}