using FluentMigrator;
using FinanceService.Domain.Entities;

namespace FinanceService.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.ADD_USER_EMAIL_NOTIFICATIONS_COLUMN, description: "Adding EmailNotificationsEnabled column to users table.")]
public class Version00000008 : ForwardOnlyMigration
{
    public override void Up()
    {
        Alter.Table(tableName: MigrationContants.TableName.USERS)
            .AddColumn(name: nameof(User.EmailNotificationsEnabled)).AsBoolean().NotNullable().WithDefaultValue(value: true);
    }
}
