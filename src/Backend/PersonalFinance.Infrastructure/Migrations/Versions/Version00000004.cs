using FluentMigrator;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_CATEGORY, description: "Creating category table registrations.")]
public class Version00000004 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.CATEGORIES)
            .WithColumn(name: nameof(Category.Id)).AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn(name: nameof(Category.Name)).AsString(size: 100).NotNullable();
    }
}