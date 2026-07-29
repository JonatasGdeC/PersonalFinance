using FluentMigrator;
using FinanceService.Domain.Entities;

namespace FinanceService.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_PARTICIPANT, description: "Creating participant table registrations.")]
public class Version00000002 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.PARTICIPANTS)
            .WithColumn(name: nameof(Participant.Id)).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: nameof(Participant.Name)).AsString(size: 100).NotNullable()
            .WithColumn(name: nameof(Participant.Image)).AsString().Nullable()
            .WithColumn(name: nameof(Participant.UserId)).AsGuid().NotNullable();
    }
}