using System.Data;
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
            .WithColumn(name: nameof(Category.Name)).AsString(size: 100).NotNullable()
            .WithColumn(name: nameof(Category.UserId)).AsGuid().NotNullable();
        
        Create.ForeignKey(foreignKeyName: "FK_Category_Users_UserId")
            .FromTable(table: MigrationContants.TableName.CATEGORIES).ForeignColumn(column: nameof(Category.UserId))
            .ToTable(table: MigrationContants.TableName.USERS).PrimaryColumn(column: nameof(User.Id))
            .OnDelete(rule: Rule.Cascade);
    }
}