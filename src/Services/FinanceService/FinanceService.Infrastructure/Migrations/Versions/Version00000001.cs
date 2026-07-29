using FluentMigrator;
using FinanceService.Domain.Entities;

namespace FinanceService.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_POTS, description: "Creating pots table registrations.")]
public class Version00000001 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.POTS)
            .WithColumn(name: nameof(Pot.Id)).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: nameof(Pot.Name)).AsString(size: 100).NotNullable()
            .WithColumn(name: nameof(Pot.CurrentAmount)).AsDouble().NotNullable()
            .WithColumn(name: nameof(Pot.Target)).AsDouble().NotNullable()
            .WithColumn(name: nameof(Pot.Color)).AsString(size: 20).NotNullable()
            .WithColumn(name: nameof(Pot.UserId)).AsGuid().NotNullable();
    }
}