using FluentMigrator;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_PASSWORD_RESET_CODE, description: "Creating Password reset codes table registrations.")]
public class Version00000002 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.PASSWORD_RESET_CODES)
            .WithColumn(name: nameof(PasswordResetCode.UserId)).AsGuid().PrimaryKey(primaryKeyName: "PK_PasswordResetCodes_UserId").NotNullable()
            .WithColumn(name: nameof(PasswordResetCode.CodeHash)).AsString(size: 100).Nullable()
            .WithColumn(name: nameof(PasswordResetCode.Attempts)).AsInt16().Nullable()
            .WithColumn(name: nameof(PasswordResetCode.ExpiresAt)).AsDateTime().NotNullable();
    }
}