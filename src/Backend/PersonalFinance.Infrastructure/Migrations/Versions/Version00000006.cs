using FluentMigrator;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_BUDGET, description: "Creating budget table registrations.")]
public class Version00000006 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.BUDGETS)
            .WithColumn(name: nameof(Budget.Id)).AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn(name: nameof(Budget.MaximumSpend)).AsDouble().NotNullable()
            .WithColumn(name: nameof(Budget.Color)).AsString(size: 20).NotNullable()
            .WithColumn(name: nameof(Budget.CategoryId)).AsInt64().NotNullable()
            .WithColumn(name: nameof(Budget.UserId)).AsGuid().NotNullable();

        Create.ForeignKey(foreignKeyName: "FK_Budgets_Categories_CategoryId")
            .FromTable(table: MigrationContants.TableName.BUDGETS).ForeignColumn(column: nameof(Budget.CategoryId))
            .ToTable(table: MigrationContants.TableName.CATEGORIES).PrimaryColumn(column: nameof(Category.Id));

        Create.ForeignKey(foreignKeyName: "FK_Budgets_Users_UserId")
            .FromTable(table: MigrationContants.TableName.BUDGETS).ForeignColumn(column: nameof(Budget.UserId))
            .ToTable(table: MigrationContants.TableName.USERS).PrimaryColumn(column: nameof(User.Id));
    }
}
