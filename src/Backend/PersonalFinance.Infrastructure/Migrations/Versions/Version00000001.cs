using FluentMigrator;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_USER, description: "Creating user table registrations.")]
public class Version00000001 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.USERS)
            .WithColumn(name: nameof(User.Id)).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: nameof(User.Name)).AsString(size: 100).NotNullable()
            .WithColumn(name: nameof(User.Email)).AsString(size: 256).NotNullable()
            .WithColumn(name: nameof(User.Password)).AsString(size: 100).Nullable()
            .WithColumn(name: nameof(User.GoogleId)).AsString(size: 50).Nullable()
            .WithColumn(name: nameof(User.ProfileImage)).AsString(size: 500).Nullable();
    }
}