using System.Data;
using FluentMigrator;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_BILL, description: "Creating bill table registrations.")]
public class Version00000007 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.BILLS)
            .WithColumn(name: nameof(Bill.Id)).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: nameof(Bill.DueDate)).AsDateTime().NotNullable()
            .WithColumn(name: nameof(Bill.Amount)).AsDouble().NotNullable()
            .WithColumn(name: nameof(Bill.InstallmentsTotal)).AsInt32().NotNullable()
            .WithColumn(name: nameof(Bill.InstallmentsPaid)).AsInt32().NotNullable()
            .WithColumn(name: nameof(Bill.CategoryId)).AsGuid().Nullable()
            .WithColumn(name: nameof(Bill.ParticipantId)).AsGuid().NotNullable()
            .WithColumn(name: nameof(Bill.UserId)).AsGuid().NotNullable();

        Create.ForeignKey(foreignKeyName: "FK_Bills_Categories_CategoryId")
            .FromTable(table: MigrationContants.TableName.BILLS).ForeignColumn(column: nameof(Bill.CategoryId))
            .ToTable(table: MigrationContants.TableName.CATEGORIES).PrimaryColumn(column: nameof(Category.Id))
            .OnDelete(rule: Rule.SetNull);

        Create.ForeignKey(foreignKeyName: "FK_Bills_Participants_ParticipantId")
            .FromTable(table: MigrationContants.TableName.BILLS).ForeignColumn(column: nameof(Bill.ParticipantId))
            .ToTable(table: MigrationContants.TableName.PARTICIPANTS).PrimaryColumn(column: nameof(Participant.Id))
            .OnDelete(rule: Rule.Cascade);

        Create.ForeignKey(foreignKeyName: "FK_Bills_Users_UserId")
            .FromTable(table: MigrationContants.TableName.BILLS).ForeignColumn(column: nameof(Bill.UserId))
            .ToTable(table: MigrationContants.TableName.USERS).PrimaryColumn(column: nameof(User.Id))
            .OnDelete(rule: Rule.Cascade);
    }
}
