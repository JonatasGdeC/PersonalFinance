using FluentMigrator;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_POTS, description: "Creating pots table registrations.")]
public class Version00000002 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.POTS)
            .WithColumn(name: nameof(Pot.Id)).AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn(name: nameof(Pot.Name)).AsString(size: 100).NotNullable()
            .WithColumn(name: nameof(Pot.CurrentAmount)).AsDouble().NotNullable()
            .WithColumn(name: nameof(Pot.Target)).AsDouble().NotNullable()
            .WithColumn(name: nameof(Pot.Color)).AsString(size: 20).NotNullable()
            .WithColumn(name: nameof(Pot.UserId)).AsGuid().NotNullable();

        Create.ForeignKey(foreignKeyName: "FK_Pots_Users_UserId")
            .FromTable(table: MigrationContants.TableName.POTS).ForeignColumn(column: nameof(Pot.UserId))
            .ToTable(table: MigrationContants.TableName.USERS).PrimaryColumn(column: nameof(User.Id));
    }
}