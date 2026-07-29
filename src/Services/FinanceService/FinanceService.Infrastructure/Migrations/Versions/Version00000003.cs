using FluentMigrator;
using FinanceService.Domain.Entities;

namespace FinanceService.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_CATEGORY, description: "Creating category table registrations.")]
public class Version00000003 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.CATEGORIES)
            .WithColumn(name: nameof(Category.Id)).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: nameof(Category.Name)).AsString(size: 100).NotNullable()
            .WithColumn(name: nameof(Category.Type)).AsInt32().NotNullable()
            .WithColumn(name: nameof(Category.UserId)).AsGuid().NotNullable();
    }
}