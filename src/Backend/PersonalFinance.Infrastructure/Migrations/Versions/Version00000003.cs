using System.Data;
using FluentMigrator;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_PARTICIPANT, description: "Creating participant table registrations.")]
public class Version00000003 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.PARTICIPANTS)
            .WithColumn(name: nameof(Participant.Id)).AsInt64().PrimaryKey().Identity().NotNullable()
            .WithColumn(name: nameof(Participant.Name)).AsString(size: 100).NotNullable()
            .WithColumn(name: nameof(Participant.Image)).AsString().Nullable()
            .WithColumn(name: nameof(Participant.UserId)).AsGuid().NotNullable();

        Create.ForeignKey(foreignKeyName: "FK_Particiapant_Users_UserId")
            .FromTable(table: MigrationContants.TableName.PARTICIPANTS).ForeignColumn(column: nameof(Participant.UserId))
            .ToTable(table: MigrationContants.TableName.USERS).PrimaryColumn(column: nameof(User.Id))
            .OnDelete(rule: Rule.Cascade);
    }
}