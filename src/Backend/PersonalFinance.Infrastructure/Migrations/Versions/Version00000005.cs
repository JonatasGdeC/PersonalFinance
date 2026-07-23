using System.Data;
using FluentMigrator;
using PersonalFinance.Domain.Entities;

namespace PersonalFinance.Infrastructure.Migrations.Versions;

[Migration(version: MigrationContants.Version.TABLE_REGISTER_TRANSACTION, description: "Creating transaction table registrations.")]
public class Version00000005 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table(tableName: MigrationContants.TableName.TRANSACTIONS)
            .WithColumn(name: nameof(Transaction.Id)).AsGuid().PrimaryKey().NotNullable()
            .WithColumn(name: nameof(Transaction.Date)).AsDateTime().NotNullable()
            .WithColumn(name: nameof(Transaction.Type)).AsInt32().NotNullable()
            .WithColumn(name: nameof(Transaction.Amount)).AsDouble().NotNullable()
            .WithColumn(name: nameof(Transaction.CategoryId)).AsGuid().Nullable()
            .WithColumn(name: nameof(Transaction.ParticipantId)).AsGuid().NotNullable()
            .WithColumn(name: nameof(Transaction.UserId)).AsGuid().NotNullable();

        Create.ForeignKey(foreignKeyName: "FK_Transactions_Categories_CategoryId")
            .FromTable(table: MigrationContants.TableName.TRANSACTIONS).ForeignColumn(column: nameof(Transaction.CategoryId))
            .ToTable(table: MigrationContants.TableName.CATEGORIES).PrimaryColumn(column: nameof(Category.Id))
            .OnDelete(rule: Rule.SetNull);

        Create.ForeignKey(foreignKeyName: "FK_Transactions_Participants_ParticipantId")
            .FromTable(table: MigrationContants.TableName.TRANSACTIONS).ForeignColumn(column: nameof(Transaction.ParticipantId))
            .ToTable(table: MigrationContants.TableName.PARTICIPANTS).PrimaryColumn(column: nameof(Participant.Id))
            .OnDelete(rule: Rule.Cascade);

        Create.ForeignKey(foreignKeyName: "FK_Transactions_Users_UserId")
            .FromTable(table: MigrationContants.TableName.TRANSACTIONS).ForeignColumn(column: nameof(Transaction.UserId))
            .ToTable(table: MigrationContants.TableName.USERS).PrimaryColumn(column: nameof(User.Id))
            .OnDelete(rule: Rule.Cascade);
    }
}
