using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.DataAccess;

internal class UserServiceDbContext(DbContextOptions options) : DbContext(options: options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<PasswordResetCode> PasswordResetCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PasswordResetCode>(buildAction: passwordResetCode =>
        {
            passwordResetCode.HasKey(keyExpression: entity => entity.UserId);
            passwordResetCode.Ignore(propertyExpression: entity => entity.IsValid);
        });
    }
}