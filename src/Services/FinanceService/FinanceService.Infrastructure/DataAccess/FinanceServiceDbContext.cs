using Microsoft.EntityFrameworkCore;
using FinanceService.Domain.Entities;

namespace FinanceService.Infrastructure.DataAccess;

internal class FinanceServiceDbContext(DbContextOptions options) : DbContext(options: options)
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Pot> Pots { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Bill> Bills { get; set; }
}